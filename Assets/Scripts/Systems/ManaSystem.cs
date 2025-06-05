using System;
using System.Collections;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    private const int MAX_MANA = 3;
    private int currentMana = MAX_MANA;
    
    [SerializeField]private ManaUI manaUI;

    private void OnEnable()
    {
        manaUI.UpdateMana(currentMana);
        
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);
        
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostRaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.DetachPerformer<RefillManaGA>();
        
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostRaction, ReactionTiming.POST);
    }
    
    public bool HasEnoughMana(int amount)
    {
        return currentMana >= amount;
    }

#region Performers->执行者
    private IEnumerator SpendManaPerformer(SpendManaGA spendMana)
    {
        currentMana -= spendMana.Amount;
        manaUI.UpdateMana(currentMana);
        yield return null;
    }
    
    private IEnumerator RefillManaPerformer(RefillManaGA refillMana)
    {
        currentMana = MAX_MANA;
        manaUI.UpdateMana(currentMana);
        yield return null;
    }
#endregion

#region Reaction->反应
    private void EnemyTurnPostRaction(EnemyTurnGA obj)
    {
        RefillManaGA refillMana = new RefillManaGA();
        ActionSystem.Instance.AddReaction(refillMana);
    }
#endregion
    
}
