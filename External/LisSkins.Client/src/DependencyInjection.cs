using LisSkinsClient.Abstractions;
using LisSkinsClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LisSkinsClient;

/// <summary>
/// Зависимости
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавление Lis-Skins клиента
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection AddLisSkinsClient(this IServiceCollection services)
    {
        services.AddHttpClient<ILisSkinsClient, ApiClient>((provider, client) =>
        {
            var config = provider.GetRequiredService<IConfiguration>();

            var baseUrl = new Uri(config.GetSection("Lis-Skins:BaseUrl").Value ??
                                  throw new InvalidOperationException("Set Base Url for API Lis-Skins"));

            var authorize = $"Bearer {config.GetSection("Lis-Skins:Token").Value ??
                                      throw new InvalidOperationException("Set Token For API Lis-Skins")}";
            
            Console.WriteLine($"ETO TEST: {authorize}");

            client.DefaultRequestHeaders.Add("Authorization", authorize);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            client.BaseAddress = baseUrl;
        });
        
        return services;
    }
}