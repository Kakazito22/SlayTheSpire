using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetUpSystem : MonoBehaviour
{
    [SerializeField]private List<CardData> cardData;

    // Start is called before the first frame update
    void Start()
    {
        CardSystem.Instance.SetUp(cardData);
        DrawCardsGA drawCardsGA = new DrawCardsGA(5);
        ActionSystem.Instance.Perform(drawCardsGA);
    }
}
