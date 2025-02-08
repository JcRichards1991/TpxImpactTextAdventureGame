using Microsoft.Extensions.DependencyInjection;
using TextAdventureGame.Services;

namespace TextAdventureGame.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTextAdventureGame(this IServiceCollection services)
    {
        services.AddScoped<IGameEngineService, GameEngineService>();

        return services;
    }
}