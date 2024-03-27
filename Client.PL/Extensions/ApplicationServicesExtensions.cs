using Client.BLL.Interfaces;
using Client.BLL.Repositories;
using Client.PL.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Client.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));

            return services;
        }
    }
}
