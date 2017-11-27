using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace SwitchBit.Web.Jwt
{
    public class TokenSecureDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string algo;
        private readonly TokenValidationParameters validationParams;

        public TokenSecureDataFormat(string algorithm, TokenValidationParameters tokenValidationParameters)
        {
            algo = algorithm;
            validationParams = tokenValidationParameters;
        }

        public string Protect(AuthenticationTicket data)
        {
            throw new NotImplementedException(); //TODO: Implement JwtDataFormat.Protect(data)
        }

        public string Protect(AuthenticationTicket data, string purpose)
        {
            throw new NotImplementedException(); //TODO: Implement JwtDataFormat.Protect(data, purpose)
        }

        public AuthenticationTicket Unprotect(string protectedText) 
            => Unprotect(protectedText, null);

        public AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = null;

            try
            {
                principal = handler.ValidateToken(protectedText, validationParams, out SecurityToken validToken);
                var jwt = validToken as JwtSecurityToken;
                if (jwt == null)
                    throw new ArgumentNullException(); //No sense saying what, logs or something else should

                if (!jwt.Header.Alg.Equals(algo, StringComparison.Ordinal))
                    throw new ArgumentException("ARG ERROR"); //invalid algorithm

                //TODO: Customize the JWT validation more here. Check request it came from etc so we can't get brute forced?
            }
            catch (SecurityTokenValidationException ve)
            {
                Debug.WriteLine($@"[Web.Jwt.TokenSecureDataFormat] Unprotect exception: {ve.Message}");
            }
            catch (ArgumentException ae)
            {
                Debug.WriteLine($@"[Web.Jwt.TokenSecureDataFormat] Argument exception: {ae.Message}");
            }

            return new AuthenticationTicket(principal, new AuthenticationProperties(), "Cookie");
        }
    }
}