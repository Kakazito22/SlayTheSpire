/// <summary>
/// 卡牌效果类
/// 每个效果都是一个 GameAction
/// </summary>
[System.Serializable]
public abstract class Effect
{
    public abstract GameAction GetGameAction();
}
