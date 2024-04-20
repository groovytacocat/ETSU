using System;
using System.Security.Cryptography;

namespace PassMan.Data
{
    public class User : Login
    {
        /// <summary>
        /// <see cref="bool"/> acting as flag to verify successful authentication
        /// </summary>
        public bool Auth { get; private set; }

        /// <summary>
        /// Collection of <see cref="User"/>'s <see cref="SiteLogin"/> objects using the <see cref="string"/> name of the <see cref="SiteLogin"/> as a key
        /// </summary>
        public Dictionary<string, SiteLogin> Vault { get; private set; }

        /// <summary>
        /// <see cref="string"/> Representing the <see cref="Login.username"/> of the <see cref="User"/> object
        /// </summary>
        public string MasterName { get { return this.username; } }

        /// <summary>
        /// <see cref="string"/> Representing the salted hash of <see cref="User"/>'s <see cref="Login.password"/>
        /// </summary>
        public string MasterPass { get { return this.password; } }

        /// <summary>
        /// <see cref="string"/> representing the salt value for hashing the <see cref="User.MasterPass"/>
        /// </summary>
        private readonly string salt = "CSCI1260";

        /// <summary>
        /// Method to compute the <see cref="SHA256"/> hash of the <see cref="User.MasterPass"/>
        /// Salts the <paramref name="inPass"/> with <see cref="salt"/> converts to an array of <see cref="byte"/>
        /// Stores the hashed <see cref="byte"/> array.
        /// </summary>
        /// <param name="inPass"><see cref="string"/> Representing <see cref="User.MasterPass"/></param>
        /// <returns><see cref="string"/> that contains the salted and hashed <see cref="User.MasterPass"/></returns>
        public string HashPass(string inPass)
        {
            string preHashString = inPass + this.salt;

            byte[] preHashBytes = System.Text.Encoding.UTF8.GetBytes(preHashString);

            byte[] hashBytes = SHA256.HashData(preHashBytes);

            return System.Text.Encoding.UTF8.GetString(hashBytes);
        }

        /// <summary>
        /// Override of <see cref="Login.SetPass(string)"/>
        /// If valid, calls <see cref="HashPass(string)"/> then assigns that value to <see cref="Login.password"/>
        /// </summary>
        /// <inheritdoc/>
        public override string SetPass(string inPass)
        {
            try
            {
                base.CheckPass(inPass);
            }
            catch (Exception e)
            {
                return $"ERROR: {e.Message}";
            }

            this.password = HashPass(inPass);

            return "Password set successfully";
        }

        /// <summary>
        /// Adds <see cref="SiteLogin"/> objects to <see cref="Vault"/>
        /// Uses Try-Catch to ensure that <paramref name="siteName"/> isn't blank
        /// If valid <paramref name="siteName"/> creates a <see cref="SiteLogin"/> object with provided values
        ///     Calls <see cref="Login.SetPass(string)"/> and passes that value to <see cref="string"/> variable
        /// Performs a check to ensure that the password was valid
        ///     Adds <see cref="SiteLogin"/> to <see cref="Vault"/>
        ///     Overwrites <see cref="KeyValuePair"/> for the specified <see cref="SiteLogin"/> if present
        ///     Otherwise adds to <see cref="Vault"/>
        /// </summary>
        /// <param name="siteName"><see cref="string"/> Representing name of <see cref="SiteLogin"/></param>
        /// <param name="siteURL"><see cref="string"/> Representing URL of <see cref="SiteLogin"/></param>
        /// <param name="siteUser"><see cref="string"/> Representing username of <see cref="SiteLogin"/></param>
        /// <param name="sitePass"><see cref="string"/> Representing password of <see cref="SiteLogin"/></param>
        /// <returns><see cref="string"/> Representing success or failure status</returns>
        public string AddLogin(string siteName, string siteURL, string siteUser, string sitePass)
        {
            try
            {
                if(siteName == "")
                {
                    return "ERROR: Site name cannot be blank";
                }

                SiteLogin site = new SiteLogin(siteURL, siteUser, sitePass);

                string valid = site.SetPass(sitePass);

                if(siteURL.StartsWith("ERROR") || valid.StartsWith("ERROR"))
                {
                    return valid;
                }
                else
                {
                    Vault[siteName] = new SiteLogin(siteURL, siteUser, sitePass);
                }
            }
            catch(ArgumentNullException nullE)
            {
                return $"ERROR: {nullE.Message}";
            }
            catch(ArgumentException argE)
            {
                return $"ERROR: {argE.Message}";
            }

            return $"Login for {siteName} at {siteURL} added successfully";
        }

        /// <summary>
        /// Helper function for Service to mark a <see cref="User"/> as successfully authenticated
        /// </summary>
        public void LogInUser()
        {
            this.Auth = true;
        }

        /// <summary>
        /// Uses Try-Catch to remove <see cref="SiteLogin"/>
        /// Removes <see cref="SiteLogin"/> from <see cref="Vault"/>
        /// Otherwise catches <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="siteName"><see cref="string"/> Representing <see cref="SiteLogin"/> name</param>
        /// <returns><see cref="string"/> Representing Success or Failure status</returns>
        public string RemoveLogin(string siteName)
        {
            try
            {
                Vault.Remove(siteName);
            }
            catch(ArgumentNullException argNull)
            {
                return $"ERROR: {argNull.Message}";
            }

            return $"Login for {siteName} removed succesfully";
        }

        /// <summary>
        /// Constructor for <see cref="User"/> object taking only <paramref name="inName"/>
        /// Instantiates an empty <see cref="Vault"/>
        /// Sets <see cref="Auth"/> as false by default
        /// </summary>
        /// <param name="inName"><see cref="string"/> Representing the <see cref="User.MasterName"/></param>
        public User(string inName) : base(inName, "")
        {
            this.Vault = new Dictionary<string, SiteLogin>();
            this.Auth = false;
        }
    }
}

