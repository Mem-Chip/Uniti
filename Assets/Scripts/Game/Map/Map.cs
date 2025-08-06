using System.Collections.Generic;
using UnityEngine;

public class Map :
    MonoBehaviour,
    ICreatable<Dictionary<GridPosition, Tile>>
{
    private Dictionary<GridPosition, Tile> _data;
    public Dictionary<GridPosition, Tile> Data { get => _data; private set => _data = value; }

    public GameObject Initialize(Dictionary<GridPosition, Tile> data)
    {
        Data = data;
        return gameObject;
    }
    
    public Tile GetTileAt(GridPosition position)
    {
        if (_data.TryGetValue(position, out Tile tile))
        {
            return tile;
        }
        return null;
    }
    public Entity GetEntityAt(GridPosition position)
    {
        Tile tile = GetTileAt(position);
        if (tile == null) return null;
        return tile.EntityOnTile;
    }
}