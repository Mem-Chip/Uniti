using System.Collections.Generic;

/* 
    Stat<T>泛型类
        用于表示各种有最大值和当前值的实体属性
        公共成员变量:
            T Max: 当前最大值
            T Current: 当前值
        公共方法:
            SetMax(T value) 设置最大值，自动限制当前值
            SetCurrent(T value) 设置当前值，自动限制当前值
            ModifyCurrent(T delta) 调整当前值，输入变化量，支持正负，调用SetCurrent以设置最终当前值
   
*/
public class Stat<T> where T : struct
{
    public T BaseMax { get; }           // 基础最大值

    private T _max;
    public T Max
    {
        get => _max;
        set { SetMax(value); }
    }                                   // 最大值
    private T _current;
    public T Current
    {
        get => _current;
        set { SetCurrent(value); }
    }                                   // 当前值

    public Stat(T baseMax)
    {
        this.BaseMax = baseMax;
        _max = baseMax;
        _current = baseMax;
    }

    public void SetMax(T value)     //设置最大值
    {
        //防止当前值超过最大值
        _max = value;
        if (Comparer<T>.Default.Compare(_current, _max) > 0)
        {
            _current = _max;
        }
    }

    public void ModifyCurrent(T delta)  //调整当前值
    {
        dynamic cur = _current;
        dynamic d = delta;
        T finalCurrent = cur + d;

        SetCurrent(finalCurrent);
    }

    public void SetCurrent(T value)  //设置当前值
    {
        _current = value;
        if (Comparer<T>.Default.Compare(_current, default) < 0)  //限制不小于最小值
            _current = default;
        if (Comparer<T>.Default.Compare(_current, BaseMax) > 0)  //限制不大于最大值
            _current = BaseMax;
    }
}
