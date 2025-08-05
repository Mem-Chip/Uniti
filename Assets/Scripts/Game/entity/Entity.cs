using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityData stats;
    public bool isOnTurn = false;
    public void commitAction()
    {

    }
    public void endTurn()
    {
        BattleManager.Instance.EndTurn(this);
        isOnTurn = false;
    }
}
