using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    private readonly List<CardView> cards = new();
    public IEnumerator AddCard(CardView card)
    {
        cards.Add(card);

        yield return UpdateCardPos(0.15f);
    }
    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);
        if (cardView == null)
            return null;
        cards.Remove(cardView);
        StartCoroutine(UpdateCardPos(0.15f));
        return cardView;
    }

    private CardView GetCardView(Card card)
    {
        return cards.FirstOrDefault(cardView => cardView.Card == card);
    }
    private IEnumerator UpdateCardPos(float duration)
    {
        if (cards.Count == 0) yield break;

        Spline spline = splineContainer.Spline;
        float step = 1f / 10f;
        float firstCardPosition = 0.5f - (cards.Count - 1) * step / 2f;
        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardPosition + i * step;
            Vector3 splinePos = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            cards[i].transform.DOMove(splinePos + transform.position + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        yield return new WaitForSeconds(duration);
    }
}
