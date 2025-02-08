using Microsoft.Extensions.Options;
using TextAdventureGame.Models;

namespace TextAdventureGame.Services;

internal class GameEngineService(IOptions<GameSettings> gameSettings) : IGameEngineService
{
    private readonly GameSettings _gameSettings = gameSettings.Value;

    private Room? _currentRoom;

    private int _playerHealth;

    private int _roomCompletedCount;

    public void Initailise()
    {
        _playerHealth = _gameSettings.PlayerStartingHealth;
        _roomCompletedCount = 0;

        Console.WriteLine($"Starting Health: {_playerHealth}");
        Console.WriteLine($"Rooms to complete to win: {_gameSettings.RoomsToCompleteToWin}");

        GetStartingRoom();

        OutputRoom();
    }

    private void GetStartingRoom()
    {
        var rnd = new Random();
        var index = rnd.Next(0, _gameSettings.Rooms.Count);

        _currentRoom = _gameSettings.Rooms.ElementAt(index);
    }

    private void OutputRoom()
    {
        Console.WriteLine(_currentRoom!.FlavourText);
        Console.WriteLine(_currentRoom.Question);

        for (var i = 0; i < _currentRoom.Choices.Count; i++)
        {
            var choice = _currentRoom.Choices.ElementAt(i);

            Console.WriteLine($"> {i + 1}. {choice.Text}");
        }
    }

    public void ProcessInput(string userInput)
    {
        if (!int.TryParse(userInput, out var index))
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            return;
        }

        if (index <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a number greater than 0.");
            return;
        }

        if (index > _currentRoom!.Choices.Count)
        {
            Console.WriteLine("Invalid input. Please enter a number less than or equal to the choice you'd like to pick");
            return;
        }

        ProcessUserChoice(_currentRoom.Choices.ElementAt(index - 1));
    }

    private void ProcessUserChoice(RoomOutcome userChoice)
    {
        ProccessOutcomeToUser(userChoice);

        if (_playerHealth == 0)
        {
            EndGame("OH NO! you've lost.");
            return;
        }

        _roomCompletedCount++;

        if (_roomCompletedCount == _gameSettings.RoomsToCompleteToWin)
        {
            EndGame("CONGRATULATIONS, you've won.");

            return;
        }

        OutputRoomOutcom(userChoice);

        GetNextRoom(userChoice);
        OutputRoom();
    }

    private void ProccessOutcomeToUser(RoomOutcome userChoice)
    {
        switch (userChoice.PlayerOutcome)
        {
            case Enums.PlayerOutcome.None:
                break;

            case Enums.PlayerOutcome.LoseHealth:
                if (userChoice.HealthModifier == 0)
                {
                    _playerHealth = 0;
                }
                else
                {
                    _playerHealth -= userChoice.HealthModifier;
                }

                break;

            case Enums.PlayerOutcome.GainHealth:
                if (userChoice.HealthModifier == 0)
                {
                    _playerHealth = _gameSettings.PlayerStartingHealth;
                }
                else
                {
                    _playerHealth += userChoice.HealthModifier;
                }

                break;
        };
    }

    private void OutputRoomOutcom(RoomOutcome userChoice)
    {
        Console.WriteLine(userChoice.OutcomeText);
        Console.WriteLine($"Your health is now {_playerHealth}");
    }

    private void EndGame(string text)
    {
        Console.WriteLine(text);
        Console.WriteLine("restart or exit");
    }

    private void GetNextRoom(RoomOutcome userChoice)
    {
        var rnd = new Random();
        var next = rnd.Next(0, userChoice.ConnectingRoomIds.Count);
        var newRoomId = userChoice.ConnectingRoomIds.ElementAt(next);

        _currentRoom = _gameSettings.Rooms.First(x => x.Id == newRoomId);
    }
}