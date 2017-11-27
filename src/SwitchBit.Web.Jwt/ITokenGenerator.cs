using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SwitchBit.Web.Jwt
{
    public interface ITokenGenerator
    {
        (string access_token, int expires_in) Generate(string userName, string email, params Claim[] providedClaims);
    }
}