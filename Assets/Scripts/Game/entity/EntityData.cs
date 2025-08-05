using UnityEngine;

public struct Resource<T> {
    T max;
    T now;

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

public struct Resources {
    Resource<int> action;
    Resource<float> speed;
}

public struct EntityData
{
    public int health;
    public int healthMax;
    public GridPosition position;
    public string entityName;
    public Resources resources;
}
