public struct EntityData
{
    public GamePosition position;
    public string entityName;

    public Stat<int> health;
    public int initiative;

    public EntityData(
        Stat<int> health,
        string entityName = "",
        GamePosition position = new GamePosition(),
        int initiative = 10
    )
    {
        this.health = health;
        this.entityName = entityName;
        this.position = position;
        this.initiative = initiative;
    }
}