using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using System.Security.Principal;

namespace SwitchBit.Web.Jwt
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenGeneratorOptions _options;
        private readonly Func<string, string, ClaimsIdentity> _fetcher;

        public TokenProviderMiddleware(RequestDelegate next, IOptions<TokenGeneratorOptions> options)
        {
            _next = next;
            _options = options.Value;
            _fetcher = _options.PasswordValidator;
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
                return _next(context); //nothing to do here, move along

            Console.WriteLine($@"[Web.Jwt.TokenProviderMiddleware.Invoke][REQUEST] {context.Request.Path} | [AUTH_ENDPOIONT] {_options.Path}");

            if (context.Request.Method != "POST" || !context.Request.HasFormContentType)
            {
                Console.WriteLine($@"[Web.Jwt.TokenProviderMiddleware.Invoke][STATUS] 400 [METHOD] {context.Request.Method} - Should be POST, HasFormContentType {context.Request.HasFormContentType} - Should be True");
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad Request");
            }
            return GenerateTokenAsync(context);
        }

        private async Task GenerateTokenAsync(HttpContext context)
        {
            Console.WriteLine($@"[Web.Jwt.TokenProviderMiddleware.GenerateTokenAsync][TOKEN] Generating token...");
            var user = context.Request.Form["username"];
            var pass = context.Request.Form["password"];

            Console.WriteLine($@"[Web.Jwt.TokenProviderMiddleware.GenerateTokenAsync][TOKEN] Received username: {user} password:{pass}");
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass) || user == "undefined" || pass == "undefined")
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Bad Request");
                return;
            }

            if (_fetcher == null)
                Console.WriteLine($@"[Web.Jwt.TokenProviderMiddleware.GenerateTokenAsync][TOKEN] Authentication Delegate is null, using FAKE DEBUG IDENTITY");

            ClaimsIdentity identity;
            if (_fetcher != null)
            {
                Console.WriteLine($@"[Web.Jwt.TokenProviderMiddleware.GenerateTokenAsync][TOKEN] Calling Func<string, string, GenericIdentity>() ON {_fetcher.Target.GetType().FullName}");

                identity = _fetcher(user, pass); //NOTE: Pass in the func to the constructor for production
            }
            else
                identity = GetIdentity(user, pass); //NOTE: Debug only! test/test123

            if (identity == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Nope...");
                return;
            }

            var nowUtc = DateTime.UtcNow;
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            var unixDateTime = (nowUtc.ToUniversalTime() - unixEpoch).TotalSeconds;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, unixDateTime.ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: nowUtc,
                expires: nowUtc.Add(_options.ExpirationMinutes),
                signingCredentials: _options.SigningCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_options.ExpirationMinutes.TotalSeconds
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings()));
        }

        private ClaimsIdentity GetIdentity(StringValues user, StringValues pass)
        {
            Console.WriteLine($@"[Web.Jwt.TokenProviderMiddleware][TOKEN] Returning debug identity if credentials match the hard coded test credentials");
            if (user == "test" && pass == "test123") //TODO: ***REMOVE THIS*** DEBUG ADMIN ACESS
            {
                Console.WriteLine($@"[Web.Jwt.TokenProviderMiddleware.GetIdentity({user}, {pass})] **DEBUG** USING U:test P:test123 == true - creating new GenericIdentity({user}, {JwtDefaults.TokenProviderUserClaimsBase}");
                return new ClaimsIdentity(JwtDefaults.TokenProviderUserClaimsBase);
            }
            return null;
        }
    }

    public static class TokenProviderMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokens(this IApplicationBuilder builder, TokenGeneratorOptions options)
        {
            return builder.UseMiddleware<TokenProviderMiddleware>(Options.Create(
                new TokenGeneratorOptions
                {
                    PasswordValidator = (user, pass) => {
                        //TODO: ***REMOVE THIS*** DEBUG ADMIN ACESS
                        //TODO: Actually validate the username & password with the Identity classes
                        if (user == "test" && pass == "test")
                        {
                            Console.WriteLine($@"[Web.Jwt.TokenProviderMiddlewareExtensions.PasswordValidator({user}, {pass})] **DEBUG** USING U:test P:test == true - creating new GenericIdentity({user}, {JwtDefaults.TokenProviderUserClaimsBase}");
                            return new GenericIdentity(user, JwtDefaults.TokenProviderUserClaimsBase);
                        }
                        return null;
                    }
                }
            ));
        }
    }
}