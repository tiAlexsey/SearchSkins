using Application.Abstractions;
using FluentValidation;
using Infrastructure.Decorator;
using Infrastructure.Services;
using LisSkinsClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices()
        {
            services
                .AddLisSkinsClient()
                .AddScoped<IApiClient, ApiClient>()
                .AddApplicationHandlers();

            return services;
        }

        private IServiceCollection AddApplicationHandlers()
        {
            var assembly = typeof(IHandler<,>).Assembly;
            var handlerTypes = assembly.GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .SelectMany(t => t.GetInterfaces(), (t, i) => new { Implementation = t, Interface = i })
                .Where(x => x.Interface.IsGenericType && x.Interface.GetGenericTypeDefinition() == typeof(IHandler<,>));

            foreach (var handler in handlerTypes)
            {
                services.AddScoped(handler.Implementation);

                services.AddScoped(handler.Interface, sp =>
                {
                    var impl = sp.GetRequiredService(handler.Implementation);

                    var args = handler.Interface.GetGenericArguments();
                    var requestType = args[0];
                    var responseType = args[1];

                    var validationDecoratorType = typeof(ValidationHandlerDecorator<,>).MakeGenericType(args);
                    var validators = sp.GetServices(typeof(IValidator<>).MakeGenericType(requestType));

                    var validatedHandler = Activator.CreateInstance(validationDecoratorType, impl, validators)!;

                    var loggingDecoratorType = typeof(LoggingHandlerDecorator<,>).MakeGenericType(args);
                    var loggerType = typeof(ILogger<>).MakeGenericType(loggingDecoratorType);
                    var logger = sp.GetRequiredService(loggerType);

                    var loggedHandler = Activator.CreateInstance(loggingDecoratorType, validatedHandler, logger)!;

                    return loggedHandler;
                });
            }

            return services;
        }
    }
}