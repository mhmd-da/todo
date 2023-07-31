using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Contracts.Engines;
using ToDo.Client.Entities.Requests.User;
using ToDo.Common.Static;

namespace ToDo.Business.Engines
{
    public class UserEngine : BaseEngine, IUserEngine
    {
        private readonly UserManager<MongoUser> _userManager;
        private readonly IRoleEngine _roleEngine;

        public UserEngine(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<MongoUser>>();
            _roleEngine = serviceProvider.GetRequiredService<IRoleEngine>();
        }

        public MongoUser GetByUsername(string username)
        {
            return _userManager.Users.FirstOrDefault(u => u.NormalizedUserName == username.ToUpper());
        }

        public void Create(CreateUserRequest request)
        {
            #region Validations

            if (string.IsNullOrEmpty(request.Username))
                BadRequest(Messages.UsernameRequired);

            MongoUser user = _userManager.FindByNameAsync(request.Username).Result;

            if (user != null)
                Conflict(Messages.UsedUsername);

            if (!_roleEngine.Exist(request.RoleName))
            {
                NotFound(Messages.InvalidRoleName);
            }

            #endregion Validations

            user = new MongoUser
            {
                UserName = request.Username
            };

            var result = _userManager.CreateAsync(user, request.Password).Result;

            if (result.Succeeded)
            {
                result = _userManager.AddToRoleAsync(user, request.RoleName).Result;
                if (!result.Succeeded)
                {
                    _ = _userManager.DeleteAsync(user).Result;
                    BadRequest(result.Errors.FirstOrDefault().Description);
                }
            }
            else
            {
                BadRequest(result.Errors.FirstOrDefault().Description);
            }
        }
    }
}
