using System;
using System.Text.RegularExpressions;

namespace PassMan.Data
{
    public class SiteLogin : Login
    {
        /// <summary>
        /// Flag for use in BlazorApp to show or hide the password for a given login
        /// </summary>
        public bool ShowPass { get; set; }

        /// <summary>
        /// <see cref="string"/> Representing URL for a website
        /// </summary>
        public string SiteURL { get; private set; }

        /// <summary>
        /// <see cref="string"/> Representing username for a website
        /// </summary>
        public string SiteLogon { get { return this.username; } }

        /// <summary>
        /// <see cref="string"/> Representing password for a website
        /// </summary>
        public string SitePass { get { return this.password; } }

        /// <summary>
        /// Override of <see cref="Login.CheckPass(string)"/>
        /// Checks only <paramref name="inPass"/> length and that it does not contain <see cref="Login.username"/>
        /// </summary>
        public override void CheckPass(string inPass)
        {
            if (inPass.Length < 4)
            {
                throw new ArgumentOutOfRangeException("Password does not meet minimum length requirements");
            }
            else if (inPass.Contains(this.username))
            {
                throw new ArgumentException("Password cannot contain username");
            }
        }

        /// <summary>
        /// Constructor for <see cref="SiteLogin"/>
        /// If <see cref="SiteURL"/> is not at 5 characters long changed to ERROR to be checked later
        /// Sets <see cref="ShowPass"/> to false by default for use in <see cref="Pages"/>
        /// </summary>
        /// <param name="inURL"><see cref="string"/> Representing the <see cref="SiteURL"/></param>
        /// <param name="inName"><see cref="string"/> Representing the <see cref="Login.username"/></param>
        /// <param name="inPass"><see cref="string"/> Representing the <see cref="Login.password"/></param>
        public SiteLogin(string inURL, string inName, string inPass) : base(inName, inPass)
        {
            this.SiteURL = inURL.Length < 5 ? "ERROR" : inURL;
            this.ShowPass = false;
        }

        /// <summary>
        /// Constructor that takes only a <see cref="string"/> representing the <see cref="SiteURL"/>
        /// Passes <see cref="Login.username"/> and <see cref="Login.password"/> to empty <see cref="string"/> values
        /// </summary>
        /// <param name="inURL"><see cref="string"/> Representing the <see cref="SiteURL"/></param>
        public SiteLogin(string inURL) : this(inURL, "", "")
        {
            this.ShowPass = false;
        }
    }
}

