using Crud.Application.Interface;
using Crud.Application.Services;
using Crud.Infraestructure.Interface;
using Crud.Infraestructure.Repository;

namespace Crud.API.Configuration
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
