using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();
    public List<Unit> units = new List<Unit>();
    public Dictionary<GridPosition, Tile> tileMap;
    void Start()
    {
        tileMap = new Dictionary<GridPosition, Tile>();
        foreach (Tile tile in tiles)
        {
            tileMap[tile.gridPosition] = tile;
        }
    }
    public Tile getTileAt(GridPosition position)
    {
        if (tileMap.TryGetValue(position, out Tile tile))
        {
            return tile;
        }
        return null;
    }
    public Unit getUnitAt(GridPosition position)
    {
        Tile tile = getTileAt(position);
        if (tile != null) return null;
        return tile.unitonTile;
    }
}
