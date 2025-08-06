using System;
using System.Collections;
using UnityEngine;

public struct EntityData
{
    public Stat<int> health;
    public int initiative;
    public GridPosition position;
    public string entityName;
}