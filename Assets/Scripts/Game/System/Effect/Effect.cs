using System;

public abstract class Effect
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }    //持续时间
    public int duration;                        //唯一id
    public Entity Target { get; }               //受效果者
    public Entity Effector { get; }             //施加效果者

    protected Effect(string Name, Entity target, Entity effector, int duration = -2)
    {
        Id = Guid.NewGuid();
        this.Name = Name;
        Target = target;
        Effector = effector;
        this.duration = duration;
    }

    protected Effect(Entity target, int duration)
    {
        Target = target;
        this.duration = duration;
    }

    protected virtual void CustomHandleEffect() { }     //效果自定义部分
    protected void HandleEffect()                       //效果默认部分
    {
        CustomHandleEffect();
        if (--duration == 0) DeEffect();                //小于0的都视作无限持续时间
    }

    public abstract void EnEffect();                 //添加效果
    public abstract void DeEffect();                 //去除效果
}