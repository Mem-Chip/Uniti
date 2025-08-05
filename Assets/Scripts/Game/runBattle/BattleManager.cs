using System.Collections.Generic;
using UnityEngine;


// This class manages the turn logic.
// It handles the start of the battle, calculates initiative, and manages turns
/*

The logic are as follows:

StartBattle calculates the initiative of the entities and starts the turn handling.
It calls TickTurn to set the first entity on turn.
TickTurn set up flag about the current entity in the class as EntityonTurn,
as well as the entity on turn as the isOnTurn flag.
Any other logic based on the turn system shall get its data from these two flags, and the entitiesOnBattle list.
When an entity ends its turn, it calls EndTurn,
which sets the endTurnExecuted flag to true.
This flag is checked in the Update method to determine if the turn should be ticked.
So at the very next frame, TickTurn is called to proceed to the next entity.

*/
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Entity EntityonTurn;
    public List<Entity> entitiesOnBattle = new List<Entity>();
    public bool isBattleActive = false;
    public bool endTurnExecuted = false;

    public void StartBattle(List<Entity> entities)
    {
        entitiesOnBattle = CalculateInitiative(entities);
        isBattleActive = true;
        TickTurn();
    }
    private List<Entity> CalculateInitiative(List<Entity> entities)
    {
        Debug.Log("Initiative not implemented");
        return entities;
    }
    // The tick turn method is called to handle the turn logic
    // It is called when the battle starts and when an entity ends its turn
    private void TickTurn()
    {
        // This checks if it is called at the start of the battle
        // if EntityonTurn is null, it means the battle has just started
        // Otherwise, we can pop the current entity from the list
        // and add it back to the end, as it just ended its turn
        if (EntityonTurn != null && EntityonTurn == entitiesOnBattle[0])
        {
            entitiesOnBattle.RemoveAt(0);
            entitiesOnBattle.Add(EntityonTurn);
        }
        // This sets the new entity on turn
        EntityonTurn = entitiesOnBattle[0];
        // and set the isOnTurn flag to true for the current entity
        EntityonTurn.isOnTurn = true;

        Debug.Log($"It's {EntityonTurn.name}'s turn.");
    }

    // Since no coroutine is used, we'll have to check the turn state every frame
    // This is done in the Update method
    // When a entity ends its turn, it will call EndTurn method, setting up the endTurnExecuted flag
    // This will allow us to check if the current entity has completed its action
    // and call TickTurn to proceed to the next entity
    void Update()
    {
        // When battle prerequisites are not met, we stop the battle
        if (entitiesOnBattle == null || entitiesOnBattle.Count == 0 || !isBattleActive)
        {
            Debug.LogWarning("No entities in battle.");
            EntityonTurn = null;
            isBattleActive = false;
            return;
        }
        if (isBattleActive && EntityonTurn != null && endTurnExecuted)
        {
            endTurnExecuted = false;
            TickTurn();
        }
    }

    public void EndTurn(Entity entity)
    {
        // The logic here prevents the endTurnExecuted flag from being set multiple times
        // or being called by entities that are not currently on turn
        if (EntityonTurn != null && EntityonTurn == entity && !endTurnExecuted)
        {
            endTurnExecuted = true;
            entity.isOnTurn = false;
        }
    }
}
