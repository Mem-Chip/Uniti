using System.Collections;

public interface ICanBattle
{
    public abstract void EnBattle();
    public abstract void DeBattle();
    public abstract int Getinitiative();
    public IEnumerator TurnLoop();
    public void EndTurn();
}