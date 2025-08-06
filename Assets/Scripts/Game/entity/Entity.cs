using System.Collections;
using UnityEngine;

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