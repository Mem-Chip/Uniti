public struct EntityData
{
    private GamePosition _position;
    public GamePosition Position { get => _position; set => _position = value; }
    private string _entityName;
    public string EntityName { get => _entityName; set => _entityName = value; }
    private Stat<int> _health;
    public Stat<int> Health { get => _health; set => _health = value; }
    private int _initiative;
    public int Initiative { get => _initiative; set => _initiative = value; }


    public EntityData(
        Stat<int> health = null,
        string entityName = "",
        GamePosition position = new GamePosition(),
        int initiative = 10
    )
    {
        _entityName = entityName;
        _position = position;
        _initiative = initiative;
        _health = health != null ? health : new Stat<int>(10);
    }

}