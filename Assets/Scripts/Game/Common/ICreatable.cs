using UnityEngine;

public interface ICreatable<DataType>
{
    GameObject Initialize(DataType data);
}