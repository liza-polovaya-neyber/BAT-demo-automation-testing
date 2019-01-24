using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoSalesForce.Services.Configuration
{
    public class SalesForceSection : ConfigurationSection
    {
        [ConfigurationProperty("loginUrl", IsRequired = true)]
        public string LoginUrl
        {
            get => (string)this["loginUrl"];
            set => this["loginUrl"] = value;
        }

        [ConfigurationProperty("securityToken", IsRequired = true)]
        public string SecurityToken
        {
            get => (string)this["securityToken"];
            set => this["securityToken"] = value;
        }

        [ConfigurationProperty("consumerKey", IsRequired = true)]
        public string ConsumerKey
        {
            get => (string)this["consumerKey"];
            set => this["consumerKey"] = value;
        }

        [ConfigurationProperty("consumerSecret", IsRequired = true)]
        public string ConsumerSecret
        {
            get => (string)this["consumerSecret"];
            set => this["consumerSecret"] = value;
        }

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username
        {
            get => (string)this["username"];
            set => this["username"] = value;
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get => (string)this["password"];
            set => this["password"] = value;
        }

        /// <summary>
        /// Maximum lifetime for access token is 12 hours
        /// </summary>
        [ConfigurationProperty("refreshTokenIntevalHours", DefaultValue = "12", IsRequired = true)]
        public int RefreshTokenIntevalHours
        {
            get => (int)this["refreshTokenIntevalHours"] > 12 ? 12 : (int)this["refreshTokenIntevalHours"];
            set => this["refreshTokenIntevalHours"] = value;
        }
    }
}
