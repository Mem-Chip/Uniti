using UnityEngine;

public static class Creator
{
    public static GameObject CreateNext<ItemType, DataType>(ItemType emptyItem, DataType data) where ItemType : ICreatable<DataType>
    {
        GameObject entity = Object.Instantiate(Resources.Load<GameObject>(emptyItem.PREFABPATH));   //用预制体生成实例
        return entity.GetComponent<ItemType>().Initialize(data);
    }
}