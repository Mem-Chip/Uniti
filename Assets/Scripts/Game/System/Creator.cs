using UnityEngine;

public static class Creator
{
    private const string PREFABPATH_DEFAULT = "Prefabs/";

    public static GameObject Create<ItemType, DataType>(DataType data) where ItemType : ICreatable<DataType>
    {
        GameObject entity = Object.Instantiate(Resources.Load<GameObject>(PREFABPATH_DEFAULT + nameof(ItemType)));   //用预制体生成实例
        return entity.GetComponent<ItemType>().Initialize(data);
    }
}