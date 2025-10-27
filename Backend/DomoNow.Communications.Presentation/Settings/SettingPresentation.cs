using DomoNow.Communications.Presentation.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace DomoNow.Communications.Presentation.Settings
{
    public static class SettingPresentation
    {
        public static IServiceCollection Presentation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers();
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddTransient<ApiMiddleware>();

            serviceCollection.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            return serviceCollection;
        }
    }
}