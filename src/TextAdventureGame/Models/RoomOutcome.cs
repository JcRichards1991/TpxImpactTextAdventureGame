
using TextAdventureGame.Enums;

namespace TextAdventureGame.Models;

internal class RoomOutcome
{
    public required string Text { get; init; }

    public required string OutcomeText { get; init; }

    public required IReadOnlyList<Guid> ConnectingRoomIds { get; init; }

    public PlayerOutcome PlayerOutcome { get; init; }

    public int HealthModifier { get; set; }
}