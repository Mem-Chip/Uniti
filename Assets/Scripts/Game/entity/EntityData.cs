using System;
using System.Collections;
using UnityEngine;

public delegate void RecoverResource<T>(T finalValue);

public struct Resource<T>
{
    public T max;
    public T now;

    public Resource(T max)
    {
        this.max = max;
        this.now = max;
    }
    public Resource(T max, T now)
    {
        this.max = max;
        this.now = now;
    }
}

public struct Resources
{
    public Resource<int> action;
    public Resource<float> speed;
}

public struct EntityData
{
    public int health;
    public int healthMax;
    public GridPosition position;
    public string entityName;
    public Resources resources; //角色资源
}
