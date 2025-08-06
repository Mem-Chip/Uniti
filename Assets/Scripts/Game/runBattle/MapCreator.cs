using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public void CreateMap()
    {
        throw new System.NotImplementedException();
    }

    public void test()
    {
        GameObject tilePrefab = UnityEngine.Resources.Load<GameObject>("Prefabs/Tile");
        var map = new GameObject("Map").AddComponent<Map>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var tileObject = Instantiate(tilePrefab, new Vector3(i+0.5f, 0, j+0.5f), Quaternion.identity, map.transform);
                tileObject.name = $"Tile_{i}_{j}";
                Tile tile = tileObject.AddComponent<Tile>();
                tile.gridPosition = new GridPosition(i, j);
                map.tiles.Add(tile);
            }
        }
    }
}
