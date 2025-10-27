using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using DomoNow.Communications.Infrastructure.Persistence.Context;
using DomoNow.Communications.Infrastructure.Persistence.Repositories;
using DomoNow.Communications.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomoNow.Communications.Infrastructure.Settings
{
    public static class InfrastructureSettings
    {
        public static IServiceCollection Infrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<DataBaseContext>(options => options.UseNpgsql(configuration.GetConnectionString("DataBaseConnection")));

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            return serviceCollection;
        }
    }
}
