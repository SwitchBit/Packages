using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwitchBit.Web.Jwt
{
    public static class TokenGeneratorExtensions
    {
        public static IServiceCollection AddTokens(this IServiceCollection services)
            => AddTokens(services, (opt) => {/* Use Defaults */});

        public static IServiceCollection AddTokens(this IServiceCollection services, Action<TokenGeneratorOptions> options)
        {
            var opt = new TokenGeneratorOptions();

            options(opt);

            return services.AddSingleton(typeof(ITokenGenerator), new TokenGenerator(opt)); ;
        }
    }

    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        private readonly TokenGeneratorOptions opt;

        public TokenGenerator(TokenGeneratorOptions opt) => this.opt = opt;

        public (string access_token, int expires_in) Generate(string userName, string email, params Claim[] providedClaims)
        {
            var nowUtc = DateTime.UtcNow;
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            var unixDateTime = (nowUtc.ToUniversalTime() - unixEpoch).TotalSeconds;

            var claims = new Claim[] //TODO: Initialize to the combined size so we don't have to combine the way it is
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName ?? throw new ArgumentNullException(nameof(userName))),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, unixDateTime.ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Email, email ?? throw new ArgumentNullException(nameof(email)))
            };

            var allClaims = new Claim[providedClaims.Length + claims.Length]; //combine the claims, probably a better way
            claims.CopyTo(allClaims, 0);
            providedClaims.CopyTo(allClaims, claims.Length - 1);

            var jwt = new JwtSecurityToken(
                issuer: opt.Issuer,
                audience: opt.Audience,
                claims: allClaims,
                notBefore: nowUtc,
                expires: nowUtc.Add(opt.ExpirationMinutes),
                signingCredentials: opt.SigningCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return (encodedJwt, (int)opt.ExpirationMinutes.TotalSeconds);
        }
    }
}