using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TextAdventureGame.Extensions;
using TextAdventureGame.Models;
using TextAdventureGame.Services;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);
builder.Services.Configure<GameSettings>(builder.Configuration.GetSection(nameof(GameSettings)));

builder.Services.AddTextAdventureGame();

using var host = builder.Build();

Console.WriteLine("Welcome to the Text Adventure Game! Type your commands (1, 2) to play the game. Type 'exit' to quit. 'Restart' to restart.");

var gameEngineService = host.Services.GetRequiredService<IGameEngineService>();

gameEngineService.Initailise();

string userInput;
do
{
    userInput = Console.ReadLine();
    if (userInput.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
    {
        break;
    }
    else if (userInput.Equals("restart", StringComparison.CurrentCultureIgnoreCase))
    {
        gameEngineService.Initailise();
    }
    else
    {
        gameEngineService.ProcessInput(userInput);
    }
} while (userInput?.Equals("exit", StringComparison.CurrentCultureIgnoreCase) != true);