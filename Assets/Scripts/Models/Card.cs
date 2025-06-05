using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public string Title => data.name;
    public string Description => data.Desctiption;
    public int Mana { get; private set; }
    public Sprite Icon => data.Icon;
    public List<Effect> Effects => data.Effects;

    private readonly CardData data;
    public Card(CardData cardData)
    {
        data = cardData;
        Mana = cardData.Mana;
    }
}
