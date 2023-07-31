using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Contracts.Engines;
using ToDo.Client.Entities.Requests.Auth;
using ToDo.Client.Entities.Responses.Auth;
using ToDo.Common.Configurations;
using ToDo.Common.Static;

namespace ToDo.Business.Engines
{
    public class AuthEngine : BaseEngine, IAuthEngine
    {
        private readonly UserManager<MongoUser> _userManager;
        private readonly SignInManager<MongoUser> _signInManager;

        public AuthEngine(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<MongoUser>>();
            _signInManager = serviceProvider.GetRequiredService<SignInManager<MongoUser>>();
        }

        public TokenResponse CreateToken(TokenRequest request)
        {
            var user = _userManager.FindByNameAsync(request.Username).Result;

            if (user == null)
                NotFound(string.Format(Messages.UserNotFound, request.Username));

            var result = _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false).Result;

            if (!result.Succeeded)
                UnAuthorized(string.Format(Messages.WrongPassword, request.Username));

            var token = GenerateToken(user);

            return token;
        }

        public TokenResponse GenerateToken(MongoUser user)
        {
            TimeSpan tokenTimeSpan = TimeSpan.FromMinutes(10);
            DateTime utcNow = DateTime.UtcNow;
            DateTime utcExpiry = utcNow.Add(tokenTimeSpan);

            TokenResponse response = new TokenResponse
            {
                ExpiryInMinutes = (int)tokenTimeSpan.Minutes
            };

            var userRoles = _userManager.GetRolesAsync(user).Result;

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(CustomClaimTypes.UserId, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, String.Join(',', userRoles)));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IdentityConfiguration.SigningSecret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: IdentityConfiguration.Issuer,
                audience: IdentityConfiguration.Audience,
                claims: claims,
                notBefore: utcNow,
                expires: utcExpiry,
                signingCredentials: signingCredentials
                );

            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            claims.Clear();
            claims = null;


            return response;
        }
    }
}
