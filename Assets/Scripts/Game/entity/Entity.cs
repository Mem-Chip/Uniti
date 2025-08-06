using UnityEngine;

public struct EntityConfig  //生成预制体所需参数，提供给EntityCreator
{
    public EntityData data;
}

public class Entity : MonoBehaviour
{
    public EntityData data;

    public void Initialize(EntityConfig config)
    {
        this.data = config.data;
    }
}