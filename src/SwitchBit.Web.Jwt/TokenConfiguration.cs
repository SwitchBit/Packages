using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Hosting;

namespace SwitchBit.Web.Jwt
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string AuthenticationPath { get; set; }
        public TimeSpan ExpirationMinutes { get; set; }

        internal ConfigurationRoot configRoot;
        internal string configPath;

        public TokenConfiguration() { }

        public TokenConfiguration(ConfigurationRoot root, string path = "T0k3nz")
        {
            configRoot = root;
            configPath = path;
        }
    }
}