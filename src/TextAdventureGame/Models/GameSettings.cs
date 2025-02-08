namespace TextAdventureGame.Models;

internal class GameSettings
{
    public int PlayerStartingHealth { get; init; }

    public int RoomsToCompleteToWin { get; init; }

    public required IReadOnlyCollection<Room> Rooms { get; init; }
}