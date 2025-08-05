using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Unit unitonTurn;
    public List<Unit> unitsOnBattle = new List<Unit>();

    public void StartBattle(List<Unit> units)
    {
        unitsOnBattle = CalculateInitiative(units);

        HandleTurn();
    }
    private List<Unit> CalculateInitiative(List<Unit> units)
    {
        Debug.Log("Initiative not implemented");
        return units;
    }
    private void HandleTurn()
    {
        unitonTurn = unitsOnBattle[0];
        unitsOnBattle.RemoveAt(0);
        unitsOnBattle.Add(unitonTurn);

        Debug.Log($"It's {unitonTurn.name}'s turn.");
        
    }


}
