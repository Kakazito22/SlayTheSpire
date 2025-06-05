using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardsEffect : Effect
{
    [SerializeField] private int drawAmount = 1;

    public override GameAction GetGameAction()
    {
        return new DrawCardsGA(drawAmount);
    }
}
