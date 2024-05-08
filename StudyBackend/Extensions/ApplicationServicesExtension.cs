using System.Globalization;
using e_parliament.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using BackEndStructuer.DATA;
using BackEndStructuer.Helpers;
using BackEndStructuer.Helpers.OneSignal;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;

namespace BackEndStructuer.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<AuthorizeActionFilter>();

            // here to add
services.AddScoped<IQuestionServices, QuestionServices>();
            services.AddScoped<ISettingServices, SettingServices>();
            services.AddScoped<IDegreeFieldServices, DegreeFieldServices>();
            services.AddScoped<IUniversityDegreeServices, UniversityDegreeServices>();
            services.AddScoped<IFieldServices, FieldServices>();
            services.AddScoped<IDegreeServices, DegreeServices>();
            services.AddScoped<IUniversityServices, UniversityServices>();
            services.AddScoped<ICountryServices, CountryServices>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<DbInitializer>();

            var serviceProvider = services.BuildServiceProvider();
            
            
            var permissionSeeder = serviceProvider.GetService<DbInitializer>();
            permissionSeeder.Initialize(serviceProvider);

          



            return services;
        }
    }
}
