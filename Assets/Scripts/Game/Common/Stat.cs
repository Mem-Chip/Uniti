using System.Collections.Generic;

/* 
    Stat<T>泛型类
        用于表示各种有最大值和当前值的实体属性
        成员变量:
            T baseMax: 基础最大值
            T max: 当前最大值
            T current: 当前值
        方法:
            SetMax(T value) 设置最大值，自动限制当前值
            SetCurrent(T value) 设置当前值，自动限制当前值
            ModifyCurrent(T delta) 调整当前值，输入变化量，支持正负，调用SetCurrent以设置最终当前值
   
*/
public class Stat<T> where T : struct
{
    public T baseMax;       // 基础最大值
    public T max;           // 最大值
    public T current;       // 当前值

    public Stat(T value)
    {
        baseMax = value;
        max = value;
        current = value;
    }

    public void SetMax(T value)     //设置最大值
    {
        //防止当前值超过最大值
        max = value;
        if (Comparer<T>.Default.Compare(current, max) > 0)
        {
            current = max;
        }
    }

    public void ModifyCurrent(T delta)  //调整当前值
    {
        dynamic cur = current;
        dynamic d = delta;
        T finalCurrent = cur + d;

        SetCurrent(finalCurrent);
    }

    public void SetCurrent(T value)  //设置当前值
    {
        current = value;
        if (Comparer<T>.Default.Compare(current, default) < 0)  //限制不小于最小值
            current = default;
        if (Comparer<T>.Default.Compare(current, baseMax) > 0)  //限制不大于最大值
            current = baseMax;
    }
}
