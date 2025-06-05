using UnityEngine;

public class Card
{
    public string Title => data.Title;
    public string Description => data.Desctiption;
    public int Mana { get; private set; }
    public Sprite Icon => data.Icon;

    private readonly CardData data;
    public Card(CardData cardData)
    {
        data = cardData;
        Mana = cardData.Mana;
    }
}
