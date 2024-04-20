using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Components.Forms;
using PassMan.Data;

namespace PassMan.Services
{
    public class PassService : IPassMan
    {
        /// <inheritdoc/>
        public Dictionary<string, User> userDict { get; set; } = new Dictionary<string, User>();

        /// <inheritdoc/>
        public string AuthPass(string inPass, User user)
        {
            return user.HashPass(inPass) == user.MasterPass ? $"Successfully authenticated for {user.MasterName}" : "Incorrect Password";
        }

        /// <inheritdoc/>
        public string RegisterUser(string inName, string inPass)
        {
            User newUser = new User(inName);
            string userPass = newUser.SetPass(inPass);

            if (userDict.ContainsKey(inName) || userPass.StartsWith("ERROR"))
            {
                return userPass.StartsWith("ERROR") ? userPass : "ERROR: Username already exists";
            }

            newUser.LogInUser();

            userDict.Add(inName, newUser);

            return "Success";
        }

        /// <summary>
        /// <see cref="Task"/><<see cref="string"/>> for async <see cref="CryptoStream"/> operations
        /// Tries
        ///     Using block to create a <see cref="FileStream"/> object based on the filepath using <see cref="User.MasterName"/>.txt
        ///     Using block to create an <see cref="Aes"/> object for cryptographic operations
        ///         Async writes IV to beginning of file (This was per C# docs on how to use <see cref="FileStream"/> encryption
        ///     Using block to create a <see cref="CryptoStream"/> Object for cryptographic operations
        ///     Using block to create a <see cref="StreamWriter"/> Object for write operations
        ///     Async writes <see cref="User.MasterName"/> and <see cref="User.MasterPass"/>
        ///     Iterates through <see cref="User.Vault"/> and async writes each <see cref="KeyValuePair"/> as a comma-delimited line until complete
        /// Catches
        ///     Any <see cref="Exception"/> and provides that message to the User
        ///
        /// Upon completion: Success message is returned to user.
        /// </summary>
        /// <param name="user"><see cref="User"/> whose <see cref="User.Vault"/> is being exported</param>
        /// <returns><see cref="string"/> unwrapped from <see cref="Task"/> result denoting success/failure status of Export</returns>
        public async Task<string> ExportUser(User user)
        {
            string filePath = $@"./Data/{user.MasterName}.csv";
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    using (Aes aes = Aes.Create())
                    {
                        await fs.WriteAsync(aes.IV, 0, aes.IV.Length);

                        using (CryptoStream crypt = new CryptoStream(fs, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (StreamWriter wr = new(crypt))
                            {
                                wr.WriteLine();
                                foreach (KeyValuePair<string, SiteLogin> login in user.Vault)
                                {
                                   await wr.WriteAsync($"{login.Key}|{login.Value.SiteURL}|{login.Value.SiteLogon}|{login.Value.SitePass},");
                                }
                            }
                        }

                        string keySave = SaveKeys(aes, user);

                        if (keySave.StartsWith("ERROR"))
                        {
                            return keySave;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return $"ERROR Export - Reason: {e.Message}";
            }

            return $"{user.MasterName} Vault exported succesfully";
        }

        /// <summary>
        /// Method to import a User's encrypted password vault to their profile.
        /// Reads the file storing user encryption keys to obtain the key for that particular user
        /// Reads the the encrypted csv file that contains a User's <see cref="User.Vault"/> to obtain the AES IV
        /// Creates a <see cref="CryptoStream"/> and <see cref="StreamReader"/> to read all the data from the file in.
        /// Splits the data into a <see cref="string"/> <see cref="List{T}"/> and then iterates through the <see cref="List{T}"/>
        /// Splitting each <see cref="string"/> into the members of a <see cref="SiteLogin"/> and adding to the <see cref="User.Vault"/>
        /// Catches any exceptions and notifies User of error.
        /// </summary>
        /// <param name="user"><see cref="User"/> that is decrypting/importing</param>
        /// <returns><see cref="string"/> message to denote success or failure</returns>
        public string DecryptVault(User user)
        {
            string filePath = @"./Data/Keys.csv";
            Aes userKey = Aes.Create();

            try
            {
                using (FileStream keyFile = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new(keyFile))
                    {
                        string line;
                        string keyString = String.Empty;

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.EndsWith(user.MasterName))
                            {
                                keyString = line.Split(",")[0];
                                break;
                            }
                        }

                        userKey.Key = Convert.FromBase64String(keyString);
                    }
                }

                using (FileStream vaultFile = new FileStream($@"./Data/{user.MasterName}.csv", FileMode.Open, FileAccess.Read))
                {
                    byte[] iv = new byte[userKey.IV.Length];
                    int numBytes = userKey.IV.Length;

                    while (numBytes > 0)
                    {
                        int n = vaultFile.Read(iv, 0, numBytes);
                        if(n == 0)
                        {
                            break;
                        }
                        numBytes -= n;
                    }

                    userKey.IV = iv;

                    using (CryptoStream cryptStream = new(vaultFile, userKey.CreateDecryptor(userKey.Key, userKey.IV), CryptoStreamMode.Read))
                    {
                        using (StreamReader decrypt = new(cryptStream))
                        {
                            string data = decrypt.ReadToEnd();

                            List<string> creds = data.Split(",").ToList<string>();

                            if (creds.Contains(""))
                            {
                                creds.RemoveAt(creds.IndexOf(""));
                            }

                            foreach (string logon in creds)
                            {
                                string[] fields = logon.Split("|");

                                user.AddLogin(fields[0].Trim(), fields[1].Trim(), fields[2].Trim(), fields[3].Trim());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return $"ERROR Decryption - Reason: {e.Message}";
            }

            return "Success";
        }

        /// <summary>
        /// Opens/Creates a csv file for <see cref="Aes"/> key storage.
        /// Searches through the primary csv to see if the user has an existing key stored already.
        /// If so replaces with the newly generated key, from <see cref="ExportUser(User)"/>
        /// Otherwise all users/keys are written to a temporary file.
        /// Once the read/write operations are completed, the overwrites the Keys.csv file with the new/current Temp.csv and deletes Temp.csv when finished.
        /// </summary>
        /// <param name="aes"><see cref="Aes"/> object that contains the generated key</param>
        /// <param name="user"><see cref="User"/> object that correspondcs to the key</param>
        /// <returns><see cref="string"/> representing success/failure status to user</returns>
        private string SaveKeys(Aes aes, User user)
        {
            string filePath = @"./Data/Keys.csv";
            string tempFile = @"./Data/Temp.csv";
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamReader sr = new(fs))
                    {
                        using (StreamWriter sw = new(tempFile))
                        {
                            string line;

                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.EndsWith(user.MasterName))
                                {
                                    //Base64 encoding used due to problems with UTF-8 not properly writing keys
                                    sw.WriteLine(line.Replace(line, $"{Convert.ToBase64String(aes.Key)},{user.MasterName}"));
                                }
                                else
                                {
                                    sw.WriteLine(line);
                                }
                            }
                        }
                    }
                }

                File.Copy(tempFile, filePath, true);
                File.Delete(tempFile);
            }
            catch (Exception e)
            {
                return $"ERROR KeyStorage - Reason: {e.Message}";
            }

            return $"Success";
        }

        /// <summary>
        /// <see cref="Task"/><<see cref="string"/>> for async <see cref="FileStream"/> operations to read from a csv file
        /// Tries
        ///     Using block to create a <see cref="FileStream"/> instance with Open/Read access
        ///     Subsequent using block to create a <see cref="StreamReader"/> object using the above <see cref="FileStream"/>
        ///     Reads csv <see cref="File"/> contents one line at a time, then splits line data, until end of file
        ///     Calls <see cref="User.AddLogin(string, string, string, string)"/> on trimmed data
        /// Catches
        ///     Any <see cref="Exception"/> object thrown by File I/O operatioins or <see cref="User.AddLogin(string, string, string, string)"/>
        ///     Formats the error message to display to user and for validation checking in <see cref="Page"/> methods
        /// </summary>
        /// <param name="user"><see cref="User"/> object representing the user who's <see cref="User.Vault"/> the csv <see cref="SiteLogin"/> are being added to</param>
        /// <param name="e"><see cref="InputFileChangeEventArgs"/> representing the <see cref="File"/> being used for read operations</param>
        /// <returns><see cref="string"/> unwrapped from <see cref="Task"/> result denoting success/failure status of Import</returns>
        public async Task<string> ImportUser(User user, InputFileChangeEventArgs e)
        {
            try
            {
                using (var fs = e.File.OpenReadStream())
                {
                    using (var sr = new StreamReader(fs))
                    {
                        string line;

                        while ((line = await sr.ReadLineAsync()) != null)
                        {
                            string[] fields = line.Split(',');

                            user.AddLogin(fields[0].Trim(), fields[1].Trim(), fields[2].Trim(), fields[3].Trim());
                        }
                    }
                }
            }
            catch (ArgumentNullException nullName)
            {
                return $"ERROR: {nullName.Message}";
            }
            catch (ArgumentException noName)
            {
                return $"ERROR: {noName.Message}";
            }
            catch (DirectoryNotFoundException badDir)
            {
                return $"ERROR: {badDir.Message}";
            }
            catch (FileNotFoundException badFile)
            {
                return $"ERROR: {badFile.Message}";
            }

            return "Succesfully imported logins";
        }
    }
}