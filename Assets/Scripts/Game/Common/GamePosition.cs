public struct GridPosition
{
    public int x;
    public int z;

    public GridPosition(int x = 0, int z = 0)
    {
        this.x = x;
        this.z = z;
    }
}

public struct GamePosition
{
    public GridPosition gridPosition;
    public float y;

    public GamePosition(int x, int z, int y = 0)
    {
        gridPosition = new GridPosition(x, z);
        this.y = y;
    }
}