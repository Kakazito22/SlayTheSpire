using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;
    private readonly List<Card> drawPile = new();
    private readonly List<Card> discardPile = new();
    private readonly List<Card> hand = new();
    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreRaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostRaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreRaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostRaction, ReactionTiming.POST);
    }

#region Public方法
    public void SetUp(List<CardData> deck)
    {
        foreach (var data in deck)
        {
            Card card = new Card(data);
            drawPile.Add(card);
        }
    }
#endregion
    
#region Performers->执行者
    private IEnumerator DrawCardsPerformer(DrawCardsGA action)
    {
        int actualAmount = Mathf.Min(action.Amount, drawPile.Count);
        int notDrawn = action.Amount - actualAmount;
        for (int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();
        }
        if (notDrawn > 0)
        {
            RefillDeck();
            for (int i = 0; i < notDrawn; i++)
            {
                yield return DrawCard();
            }
        }
    }
    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA action)
    {
        foreach (var card in hand)
        {
            discardPile.Add(card);
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }
    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        Card card = playCardGA.Card;
        if (hand.Contains(card))
        {
            hand.Remove(card);
            discardPile.Add(card);
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
            
            // 扣除卡牌的费用
            SpendManaGA spendManaGa = new SpendManaGA(card.Mana);
            ActionSystem.Instance.AddReaction(spendManaGa);
            
            // 执行卡牌的效果
            foreach (var effect in card.Effects)
            {
                PerformEffectGA effectGa = new PerformEffectGA(effect);
                ActionSystem.Instance.AddReaction(effectGa);
            }
        }
    }
#endregion

#region Reaction->反应
    private void EnemyTurnPreRaction(EnemyTurnGA enemyTurnGA)
    {
        // 在敌人回合开始前执行的逻辑 -> 丢弃所有手牌
        ActionSystem.Instance.AddReaction(new DiscardAllCardsGA());
    }
    private void EnemyTurnPostRaction(EnemyTurnGA enemyTurnGA)
    {
        // 在敌人回合结束后执行的逻辑 -> 抽5张牌
        ActionSystem.Instance.AddReaction(new DrawCardsGA(5));
    }
#endregion
    
#region Helpers->方法
    // 抽一张牌
    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);
    }
    // 补充牌库
    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
    }
    // 丢弃一张牌
    private IEnumerator DiscardCard(CardView cardView)
    {
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }
#endregion
}
