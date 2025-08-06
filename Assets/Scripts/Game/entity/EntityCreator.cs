using UnityEngine;

public class EntityCreator: MonoBehaviour
{

    private EntityData temp = new EntityData();

    void Awake()
    {
        temp.health = new Stat<int>(20);
        temp.initiative = 20;
        temp.position = new GridPosition(2, 3);
        temp.entityName = "小大厨";
    }

    public void TempCreate()
    {
        Create(temp);
    }

    public GameObject Create(EntityData data)    //创建并返回一个Entity实例
    {
        var config = new EntityConfig { data = data };

        GameObject entity = Instantiate(Resources.Load<GameObject>("Prefabs/Entity"));   //用预制体生成实例
        entity.GetComponent<Entity>().Initialize(config);   //生成
        return entity;
    }
}