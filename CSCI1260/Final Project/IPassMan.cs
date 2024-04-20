using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Components.Forms;
using PassMan.Data;

namespace PassMan.Services
{
    public interface IPassMan
    {
        /// <summary>
        /// Collection representing the <see cref="User"/> objects stored by the Service.
        /// <see cref="KeyValuePair"/> Uses <see cref="User.MasterName"/> as key and the associated <see cref="User"/> object as value
        /// </summary>
        public Dictionary<string, User> userDict { get; set; }

        /// <summary>
        /// Method to authenticate a <see cref="User"/> password
        /// Compares the salted hash of <paramref name="inPass"/> to <see cref="User.MasterPass"/>
        /// </summary>
        /// <param name="inPass"><see cref="string"/> representing the <see cref="User.MasterPass"/></param>
        /// <param name="user"><see cref="User"/> representing the <see cref="User"/> being signed in to</param>
        /// <returns><see cref="string"/> representing success/failure status of Authentication</returns>
        public string AuthPass(string inPass, User user);

        /// <summary>
        /// Method to create a <see cref="User"/> object
        /// Uses <paramref name="inName"/> as <see cref="User.MasterName"/> and <paramref name="inPass"/> as <see cref="User.MasterPass"/>
        ///
        /// If <paramref name="inName"/> and <paramref name="inPass"/> are valid <see cref="User"/> is created
        /// If <paramref name="inName"/> is not an existing key then <see cref="User"/>is added to <see cref="userDict"/>
        /// </summary>
        /// <param name="inName"><see cref="string"/> Representing desired <see cref="User.MasterName"/></param>
        /// <param name="inPass"><see cref="string"/> Representing desired <see cref="User.MasterPass"/></param>
        /// <returns><see cref="string"/> Representing success/failure status of method</returns>
        public string RegisterUser(string inName, string inPass);

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
        abstract Task<string> ExportUser(User user);

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
        abstract Task<string> ImportUser(User user, InputFileChangeEventArgs e);

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
        public string DecryptVault(User user);
    }
}
