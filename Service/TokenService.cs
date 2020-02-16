using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.AppSettings;
using Entity.Data;
using Entity.Entity;
using Entity.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using Service.Interface;

namespace Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public UserTokenVM BuildUserToken(UserVM userVM)
        {
            var jwtSetting = new JwtSetting();
            _config.Bind("JwtSetting", jwtSetting);

            //UserInfo
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", userVM.UserId.ToString()),
                new Claim("name", userVM.Name),
            };

            var expireIn = DateTime.UtcNow.AddMinutes(jwtSetting.ExpireMinutes);
            var jwtToken = GenerateJwtToken(jwtSetting, claims, expireIn);

            var tokenResponse = new UserTokenVM()
            {
                Token = jwtToken,
                ExpireIn = new DateTimeOffset(expireIn).ToUnixTimeSeconds(),
                UserName = userVM.Name
            };

            return tokenResponse;
        }

        private string GenerateJwtToken(JwtSetting jwtSetting, Claim[] claims, DateTime expireIn)
        {
            var token = new JwtSecurityToken(
                issuer: jwtSetting.Issuer,
                audience: jwtSetting.Issuer,
                signingCredentials: jwtSetting.Credentials,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireIn
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
