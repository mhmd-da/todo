using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Business.Contracts.Engines;
using ToDo.Client.Entities.Requests.User;

namespace ToDo.Business.Bootstrapper
{
    public class StartupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IRoleEngine _roleEngine;
        private IUserEngine _userEngine;

        public StartupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                _userEngine = scope.ServiceProvider.GetRequiredService<IUserEngine>();
                _roleEngine = scope.ServiceProvider.GetRequiredService<IRoleEngine>();

                PopulateRoles();
                PopulateUsers();
            }

            return Task.CompletedTask;
        }

        private void PopulateRoles()
        {
            if (!_roleEngine.Exist("Admin"))
                _roleEngine.Create("Admin");

            if (!_roleEngine.Exist("NormalUser"))
                _roleEngine.Create("NormalUser");
        }

        private void PopulateUsers()
        {
            if (_userEngine.GetByUsername("admin_user") == null)
                _userEngine.Create(new CreateUserRequest { Username = "admin_user", Password = "123456", RoleName = "Admin" });
            
            if (_userEngine.GetByUsername("normal_user_1") == null)
                _userEngine.Create(new CreateUserRequest { Username = "normal_user_1", Password = "123456", RoleName = "NormalUser" });
           
            if (_userEngine.GetByUsername("normal_user_2") == null)
                _userEngine.Create(new CreateUserRequest { Username = "normal_user_2", Password = "123456", RoleName = "NormalUser" });
        }
    }
}
