using System.Collections;
using UnityEngine;

public struct EntityConfig  //生成预制体所需参数，提供给EntityCreator
{
    public EntityData data;
}

public class Entity : MonoBehaviour
{
    public EntityData data; //实体数据

    public void Initialize(EntityConfig config)
    {
        data = config.data;
    }

    public IEnumerator OnTurn()
    {
        yield return null;
    }
}