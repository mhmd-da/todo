using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using ToDo.Common.Configurations;
using ToDo.Data.Contracts.Repositories;
using ToDo.Data.Repositories;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Services;
using ToDo.Business.Engines;
using ToDo.Business.Contracts.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using ToDo.Business.AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Data.Contracts;
using CloudGateway.Business.Contracts;
using ToDo.Business.Entities.Models;
using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;

namespace ToDo.Business.Bootstrapper
{
    public static class StartupExtensions
    {
        public static IServiceCollection Init(this IServiceCollection services)
        {
            IConfiguration configuration = BuildConfigurations();
            configuration.InitMongoDbConfiguration();
            configuration.InitIdentityConfiguration();

            services.ConfigureSwagger();

            services.ConfigureIdentity();

            services.RegisterServices();
            services.ConfigureAuthentication();
            services.AddHttpContextAccessor();

            return services;
        }

        public static WebApplication Init(this WebApplication app)
        {
            app.UseSwagger();

            app.UseRouting();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.MapControllers();

            return app;
        }

        #region Configurations

        private static IConfigurationRoot BuildConfigurations()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        #endregion Configurations

        #region Services

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<AutoMapperLoader>();

            ///Engines
            services.AddScoped<ICategoryEngine, CategoryEngine>();
            services.AddScoped<IItemEngine, ItemEngine>();
            services.AddScoped<IUserEngine, UserEngine>();
            services.AddScoped<IRoleEngine, RoleEngine>();
            services.AddScoped<IAuthEngine, AuthEngine>();
            services.AddScoped<IAdminEngine, AdminEngine>();

            ///Repositories
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IItemRepository, ItemRepository>();

            ///Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();

            ///Managers
            services.AddScoped<UserManager<MongoUser>>();
            services.AddScoped<RoleManager<MongoRole>>();

            ///Hosted Services
            services.AddHostedService<StartupService>();

            return services;
        }

        #endregion Services

        #region Mongo


        private static IConfiguration InitMongoDbConfiguration(this IConfiguration configuration)
        {
            MongoDbConfiguration.ConnectionString = configuration.GetValue<string>("MongoDbConfiguration:ConnectionString");
            MongoDbConfiguration.DatabaseName = configuration.GetValue<string>("MongoDbConfiguration:DatabaseName");

            return configuration;
        }

        #endregion Mongo

        #region Identity

        private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<MongoUser, MongoRole>(config =>
                    {
                        config.SignIn.RequireConfirmedPhoneNumber = false;
                        config.SignIn.RequireConfirmedEmail = false;
                        config.Password.RequireDigit = false;
                        config.Password.RequireLowercase = false;
                        config.Password.RequireUppercase = false;
                        config.Password.RequireNonAlphanumeric = false;
                        config.Password.RequiredLength = 1;
                        config.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!&";
                        //config.User.RequireUniqueEmail = true;
                    })
                    .AddMongoDbStores<MongoUser>(c =>
                    {
                        c.ConnectionString = MongoDbConfiguration.ConnectionString;
                    })
                    .AddDefaultTokenProviders();

            return services;
        }

        private static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = IdentityConfiguration.ValidateIssuer, 
                   ValidateAudience = IdentityConfiguration.ValidateAudience,
                   ValidateIssuerSigningKey = IdentityConfiguration.ValidateSigningSecret,
                   ValidIssuer = IdentityConfiguration.Issuer,
                   ValidAudience = IdentityConfiguration.Audience, 
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IdentityConfiguration.SigningSecret)),
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,
               };
           });

            return services;
        }

        private static IConfiguration InitIdentityConfiguration(this IConfiguration configuration)
        {
            IdentityConfiguration.ValidateIssuer = configuration.GetValue<bool>("IdentityConfiguration:ValidateIssuer");
            IdentityConfiguration.ValidateAudience = configuration.GetValue<bool>("IdentityConfiguration:ValidateAudience");
            IdentityConfiguration.ValidateSigningSecret = configuration.GetValue<bool>("IdentityConfiguration:ValidateSigningSecret");
            IdentityConfiguration.Issuer = configuration.GetValue<string>("IdentityConfiguration:Issuer");
            IdentityConfiguration.Audience = configuration.GetValue<string>("IdentityConfiguration:Audience");
            IdentityConfiguration.SigningSecret = configuration.GetValue<string>("IdentityConfiguration:SigningSecret");

            return configuration;
        }

        #endregion Identity


        #region Swagger

        private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                //The format of the version added to the route URL  
                options.GroupNameFormat = "'v'VVV";
                //Tells swagger to replace the version in the controller route  
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(
            options =>
            {
                // Resolve the temprary IApiVersionDescriptionProvider service  
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // Add a swagger document for each discovered API version  
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"{Assembly.GetEntryAssembly().GetName().Name} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = $"{Assembly.GetEntryAssembly().GetName().Name} {(description.IsDeprecated ? " - DEPRECATED" : string.Empty)}",

                    });
                }

            });

            return services;
        }

        #endregion Swagger
    }
}
