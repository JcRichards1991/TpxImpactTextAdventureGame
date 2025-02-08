namespace TextAdventureGame.Models;

internal class Room
{
    public Guid Id { get; init; }

    public required string FlavourText { get; init; }

    public required string Question { get; init; }

    public required IReadOnlyList<RoomOutcome> Choices { get; init; }
}