using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Entity EntityonTurn;
    public List<Entity> entitiesOnBattle = new List<Entity>();

    public void StartBattle(List<Entity> entities)
    {
        entitiesOnBattle = CalculateInitiative(entities);

        HandleTurn();
    }
    private List<Entity> CalculateInitiative(List<Entity> entities)
    {
        Debug.Log("Initiative not implemented");
        return entities;
    }
    private void HandleTurn()
    {
        EntityonTurn = entitiesOnBattle[0];
        entitiesOnBattle.RemoveAt(0);
        entitiesOnBattle.Add(EntityonTurn);

        Debug.Log($"It's {EntityonTurn.name}'s turn.");
        
    }


}
