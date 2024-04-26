using Client.BLL.Interfaces;
using Client.BLL;
using Client.BLL.Repositories;
using Client.PL.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Client.DAL.Models;
using Microsoft.CodeAnalysis.Options;
using System;
using Client.PL.services.EmailSender;

namespace Client.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));
            services.AddTransient<IEmailSender , EmailSender>();    

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();

            //services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            //{
            //    Options.Password.RequireDigit = true;
            //    Options.Lockout.AllowedForNewUsers = true;
            //    Options.Lockout.MaxFailedAccessAttempts = 5;
            //    Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
            //});
            //services.AddAuthentication();

            return services;
        }
    }
}
