using UnityEngine;
using DG.Tweening;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardView cardPrefab;

    public CardView CreateCardView(Card card, Vector3 pos, Quaternion rotation)
    {
        CardView cardView = Instantiate(cardPrefab, pos, rotation);
        cardView.transform.localScale = Vector3.zero;
        cardView.transform.DOScale(Vector3.one, 0.15f);
        cardView.SetUp(card);
        return cardView;
    }

    // public void DestroyCard(CardView card)
    // {
    //     if (card != null)
    //     {
    //         Destroy(card.gameObject);
    //     }
    // }
}
