using System.Collections;
using UnityEngine;

public struct EntityData
{
    private string _name;
    public string EntityName { readonly get => _name; private set => _name = value; }           //名字

    private GamePosition _position;
    public GamePosition Position { readonly get => _position; set => _position = value; }                   //游戏位置

    private Tile _tileOn;
    public Tile TileOn { readonly get => _tileOn; set => _tileOn = value; }                                 //所在砖块

    private Stat<int> _health;
    public Stat<int> Health { readonly get => _health; set => _health = value; }                            //生命

    private int _initiative;
    public int Initiative { readonly get => _initiative; set => _initiative = value; }                      //先攻
}

public class Entity :
    MonoBehaviour,
    ICreatable<EntityData>
{
    public EntityData Data;

    public GameObject Initialize(EntityData data)
    {
        Data = data;
        return this.gameObject;
    }

    public IEnumerator OnTurn()
    {
        yield return null;
    }
}