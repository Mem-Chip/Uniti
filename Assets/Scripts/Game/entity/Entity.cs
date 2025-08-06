using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EntityData
{
    public Character character;

    private string _name;
    public string EntityName { readonly get => _name; private set => _name = value; }           //名字


    //temp
    private Tile _tileOn;
    public Tile TileOn { readonly get => _tileOn; set => _tileOn = value; }                                 //所在砖块
    private int _initiative;
    public int Initiative { readonly get => _initiative; set => _initiative = value; }                      //先攻
}

public class Entity :
    MonoBehaviour,
    ICreatable<EntityData>
{
    public EntityData Data;

    public event Action OnTurnStart;     //回合开始事件
    public event Action OnTurnEnd;     //回合结束事件

    //效果栏
    public List<Effect> _effectsICast;
    private List<Effect> _effectList;


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