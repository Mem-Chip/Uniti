public struct EntityData
{
    public string entityName;
    public GamePosition position;
    public Tile tileOn;

    public Stat<int> health;
    public int initiative;

    public EntityData(
        Tile tileOn,
        Stat<int> health,
        string entityName = "",
        GamePosition position = new GamePosition(),
        int initiative = 10
    )
    {
        this.tileOn = tileOn;
        this.health = health;
        this.entityName = entityName;
        this.position = position;
        this.initiative = initiative;
    }
}