using System;
using System.Text.RegularExpressions;

namespace PassMan.Data
{
    public abstract class Login
    {
        /// <summary>
        /// Username for a given set of credentials
        /// </summary>
        protected string username;

        /// <summary>
        /// Password for a given set of credentials
        /// </summary>
        protected string password;

        /// <summary>
        /// Compares the input to ensure that password entered for <paramref name="oldPass"/> matches <see cref="password"/>
        ///     and checks to see if the <paramref name="oldPass"/> and <paramref name="newPass"/> are different
        /// If the user provided the correct <paramref name="oldPass"/> and a different <paramref name="newPass"/>
        ///     <see cref="SetPass(string)"/> is called on <paramref name="oldPass"/>
        /// </summary>
        /// <param name="oldPass"><see cref="string"/> Representing the current password</param>
        /// <param name="newPass"><see cref="string"/> Representing the desired new password</param>
        /// <returns><see cref="string"/> message notifying User of success or failure</returns>
        public virtual string ChangePass(string oldPass, string newPass)
        {
            return (oldPass == this.password && oldPass != newPass) ? SetPass(newPass) : "ERROR: Old Password does not match current";
        }

        /// <summary>
        /// Helper Function - Essentially just an exception tosser 
        /// Checks <paramref name="inPass"/> to ensure that it meets password complexity criteria
        /// If any check fails an <see cref="Exception"/> is thrown with relevant Error message
        /// </summary>
        /// <param name="inPass"><see cref="string"/> Representing the <see cref="password"/> User is attempting to set</param>
        /// <exception cref="ArgumentOutOfRangeException">For passwords that are too short</exception>
        /// <exception cref="ArgumentException">Exception for if user attempts to use a <see cref="password"/> that contains the <see cref="username"/></exception>
        /// <exception cref="ArgumentException">Exception for not meeting complexity requirements (1 Upper, 1 Lower, 1 Number, 1 Special char)</exception>
        public virtual void CheckPass(string inPass)
        {
            // Regex Pattern for password complexity - I asked ChatGPT to give me this pattern (I hope that's ok)
            string pattern = @"^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[.!@#%^&*+?]).*[A-Za-z].*$";
            if(inPass.Length < 8)
            {
                throw new ArgumentOutOfRangeException("Password does not meet minimum length requirements");
            }
            else if (inPass.Contains(this.username))
            {
                throw new ArgumentException("Password cannot contain username");
            }
            else if(!Regex.IsMatch(inPass, pattern))
            {
                throw new ArgumentException("Password must contain at least 1 lowercase, 1 uppercase, 1 number, and 1 special character (.!@#%^&*+?)");
            }
        }

        /// <summary>
        /// Calls <see cref="CheckPass(string)"/> and catches <see cref="Exception"/>s thrown if any
        /// Otherwise <see cref="password"/> is set to <paramref name="inPass"/> 
        /// </summary>
        /// <param name="inPass"><see cref="string"/> Representing User's desired <see cref="password"/></param>
        /// <returns><see cref="string"/> advising User of success or failure for password status</returns>
        public virtual string SetPass(string inPass)
        {
            try
            {
                CheckPass(inPass);
            }
            catch(Exception e)
            {
                return $"ERROR: {e.Message}";
            }

            this.password = inPass;

            return "Password set successfully";
        }

        /// <summary>
        /// Explicit Constructor for a <see cref="Login"/>
        /// </summary>
        /// <param name="inName"><see cref="string"/> Representing the <see cref="username"/></param>
        /// <param name="inPass"><see cref="string"/> Representing the <see cref="password"/></param>
        public Login(string inName, string inPass)
        {
            this.username = inName;
            this.password = inPass;
        }
    }
}