public abstract class Effect
{
    public int duration;
    private readonly Character target;

    protected Effect(Character target, int duration = -2)
    {
        this.target = target;
        this.duration = duration;
    }

    public virtual void HandleEffect()
    {
        if (--duration == 0) DeEffect();                 //小于0的都视作无限持续时间
    }

    public abstract void EnEffect();
    public abstract void DeEffect();
}