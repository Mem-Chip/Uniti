using UnityEngine;

public class Tile : MonoBehaviour
{
    private GridPosition _gridPosition;
    public GridPosition GridPosition { get => _gridPosition; private set => _gridPosition = value; }      //坐标
    private Entity _entityOnTile;
    public Entity EntityOnTile { get => _entityOnTile; private set => _entityOnTile = value; }            //在上面的实体

    public Tile(GridPosition gridPosition)
    {
        _gridPosition = gridPosition;
        _entityOnTile = null;
    }

    public bool IsEmpty()       //辅助，判断是否为空
    {
        if (_entityOnTile == null) return true;
        return false;
    }

    public void MoveEntityIn(Entity entity)
    {
        if (IsEmpty())
        {
            _entityOnTile = entity;
            entity.Data.TileOn = this;  //保证双向绑定
        }
    }
}