public abstract class Effect
{
    public int duration;
    private readonly Character target;

    protected Effect(Character target, int duration = -2)
    {
        this.target = target;
        this.duration = duration;
    }

    protected abstract void CustomHandleEffect();
    protected void HandleEffect()
    {
        CustomHandleEffect();
        if (--duration == 0) DeEffect();                 //小于0的都视作无限持续时间
    }

    protected abstract void EnEffect();
    protected abstract void DeEffect();
}