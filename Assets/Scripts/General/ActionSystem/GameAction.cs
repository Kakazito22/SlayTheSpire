using System.Collections.Generic;

public abstract class GameAction
{
    public List<GameAction> PreReactions { get; private set; } = new List<GameAction>();
    public List<GameAction> PostReactions { get; private set; } = new List<GameAction>();
    public List<GameAction> PerformedReactions { get; private set; } = new List<GameAction>();
}
