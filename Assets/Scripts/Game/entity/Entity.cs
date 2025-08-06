using System.Collections;
using UnityEngine;

public class Entity :
    MonoBehaviour,
    ICreatable<EntityData>
{
    public EntityData Data;

    public string PREFABPATH
    {
        get => "Prefabs/Entity";
    }

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