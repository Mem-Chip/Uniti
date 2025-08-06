using System;

public abstract class Effect
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }    //持续时间
    public int duration;                        //唯一id
    public Entity Target { get; }               //受效果者
    public Entity Effector { get; }             //施加效果

    protected Effect(string Name, Entity target, Entity effector = null, int duration = -2)
    {
        Id = Guid.NewGuid();
        this.Name = Name;
        Target = target;
        Effector = effector;
        this.duration = duration;
    }

    protected virtual void CustomHandleEffect() { }             //效果自定义部分
    protected void HandleEffect()                               //效果默认部分
    {
        CustomHandleEffect();
        if (--duration == 0) DeEffect();                        //小于0的都视作无限持续时间
    }

    protected abstract void CustomEnEffect();                   //添加效果自定义部分
    public void EnEffect()                                      //添加效果默认部分
    {
        Target.EffectList.Add(this);
        Effector.EffectCastList.Add(this);
        CustomEnEffect();
    }
    protected abstract void CustomDeEffect();                   //去除效果自定义部分
    public void DeEffect()                                      //去除效果默认部分
    {
        CustomDeEffect();
        Target.EffectList.Remove(this);
        Effector.EffectCastList.Remove(this);
    }
}