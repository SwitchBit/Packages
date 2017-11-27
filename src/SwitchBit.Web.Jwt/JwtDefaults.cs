using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SwitchBit.Web.Jwt
{
    public partial class JwtDefaults
    {
        public static string TokenProviderSigningKey { get; set; } = "TESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTING";
        public static readonly string TokenProviderRoute = "/api/token";
        public static readonly string TokenProviderIssuer = "";
        public static readonly string TokenProviderAudience = "Users";
        public static readonly string TokenProviderAuthenticationScheme = "Cookie";
        public static readonly string TokenProviderCookieName = "access_token";
        public static string TokenProviderAlgorithm = SecurityAlgorithms.HmacSha512;
        public static SecurityKey TokenProviderSecurityKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(TokenProviderSigningKey));
        public static readonly TimeSpan TokenProviderExpirationMinutes = TimeSpan.FromMinutes(15); //TODO: 15 minutes is probably short for most
        public static readonly SigningCredentials TokenProviderSigningCredentials = new SigningCredentials(TokenProviderSecurityKey, TokenProviderAlgorithm);
        public static readonly string TokenProviderUserClaimsBase = "Token";
    }
}