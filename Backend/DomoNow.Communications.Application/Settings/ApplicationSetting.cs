using DomoNow.Communications.Application.AutoMapper;
using DomoNow.Communications.Application.Settings.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DomoNow.Communications.Application.Settings
{
    public static class ApplicationSetting
    {
        public static IServiceCollection Application(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblyContaining<AssemblyReference>();
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationsBehavior<,>));

            services.AddValidatorsFromAssemblyContaining<AssemblyReference>();
            services.AddAutoMapper(option =>
            {
                option.AddProfile(new Mapper());
            });

            return services;
        }
    }
}