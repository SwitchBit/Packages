using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;

namespace SwitchBit.Web.Jwt
{
    public static class TokenGeneratorOptionsExtensions
    {
        public static void UseConfiguration(this TokenGeneratorOptions options, TokenConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            options.Audience = (options.Audience == config.Audience) ? options.Audience : config.Audience;
            options.Issuer = (options.Issuer == config.Issuer) ? options.Issuer : config.Issuer;
            options.Path = (options.Path == config.AuthenticationPath) ? options.Path : config.AuthenticationPath; //Section already has a path property
            options.ExpirationMinutes = (options.ExpirationMinutes == config.ExpirationMinutes) ? options.ExpirationMinutes : config.ExpirationMinutes;
            //TODO: Add in new signing cred creation here for the token generator options
        }
    }

    public class TokenGeneratorOptions
    {
        public Func<string, string, ClaimsIdentity> PasswordValidator = null;

        public string Path { get; set; } = JwtDefaults.TokenProviderRoute;
        public string Issuer { get; set; } = JwtDefaults.TokenProviderIssuer;
        public string Audience { get; set; } = JwtDefaults.TokenProviderAudience;
        public TimeSpan ExpirationMinutes { get; set; } = JwtDefaults.TokenProviderExpirationMinutes;
        public SigningCredentials SigningCredentials { get; set; } = JwtDefaults.TokenProviderSigningCredentials;
    }
}