using Microsoft.Extensions.DependencyInjection;
using UserManagement.Core.Implementation;
using UserManagement.Core.Interfaces;
using UserManagement.Persistance.Repositories.Implementation;
using UserManagement.Persistance.Repositories.Interfaces;
using UserManagement.Persistance.UnitOfWork.Implementation;
using UserManagement.Persistance.UnitOfWork.Interfaces;
using UserManagement.Utils.AutoMapper;

namespace UserManagement.Utils.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuditTrailService, AuditTrailService>();
            services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();

            return services;
        }
    }
}
