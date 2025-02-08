using TextAdventureGame.Models;

namespace TextAdventureGame.Services;

internal interface IGameEngineService
{
    void Initailise();

    void ProcessInput(string userInput);
}