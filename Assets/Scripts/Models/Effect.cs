/// <summary>
/// 卡牌效果类
/// </summary>
[System.Serializable]
public abstract class Effect
{
    public abstract GameAction GetGameAction();
}
