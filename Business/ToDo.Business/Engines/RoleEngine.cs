using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Contracts.Engines;

namespace ToDo.Business.Engines
{
    public class RoleEngine : BaseEngine, IRoleEngine
    {
        private readonly RoleManager<MongoRole> _roleManager;

        public RoleEngine(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _roleManager = serviceProvider.GetRequiredService<RoleManager<MongoRole>>();
        }

        public MongoRole Get(string roleName)
        {
            return _roleManager.Roles.FirstOrDefault(r => r.NormalizedName == roleName.ToUpper());
        }

        public bool Exist(string roleName)
        {
            return _roleManager.RoleExistsAsync(roleName).Result;
        }

        public void Create(string roleName)
        {
            MongoRole role = new MongoRole { Name = roleName};

            var result = _roleManager.CreateAsync(role).Result;
           
            if (!result.Succeeded)
            {
                BadRequest(result.Errors.FirstOrDefault().Description);
            }
        }
    }
}
