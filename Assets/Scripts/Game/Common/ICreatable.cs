using UnityEngine;

public interface ICreatable<DataType>
{
    string PREFABPATH { get; }

    GameObject Initialize(DataType data);
}