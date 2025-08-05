using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Tiles in the map
    public List<Tile> tiles = new List<Tile>();
    // Entities on the map
    public List<Entity> entities = new List<Entity>();
    // Dictionary to quickly access tiles by their grid position
    public Dictionary<GridPosition, Tile> tileMap;
    void Start()
    {
        // Initialize the tile map from the list of tiles
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
    public Entity getEntityAt(GridPosition position)
    {
        Tile tile = getTileAt(position);
        if (tile != null) return null;
        return tile.entityonTile;
    }
}
