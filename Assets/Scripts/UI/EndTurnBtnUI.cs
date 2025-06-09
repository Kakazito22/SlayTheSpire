using UnityEngine;

public class EndTurnBtnUI : MonoBehaviour
{
    public void OnClick()
    {
        EnemyTurnGA enemyTurnGA = new EnemyTurnGA();
        ActionSystem.Instance.Perform(enemyTurnGA);
    }
}
