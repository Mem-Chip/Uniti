using System.Linq;

public class Character
{
    public string name;                                     //名字
    private AbilityScores _baseAbilityScores;               //基础属性点
    public AbilityScores AbilityScores
    {
        get => GetAbilityScores();
    }

    public delegate (AbilityScoreType abilityScoreType, int bonus) CalcAbilityScoresDelegate();
    public event CalcAbilityScoresDelegate OnCalcAbilityScores;     //计算属性值事件
    private AbilityScores InvokeOnCalcAbilityScores()               //触发事件
    {
        if (OnCalcAbilityScores == null) return new();              //处理空事件
        AbilityScores bonus = new();
        OnCalcAbilityScores
            .GetInvocationList()                                    //获取所有方法
            .Select(                                                //对每个方法
                f => ((CalcAbilityScoresDelegate)f).Invoke()        //创建为委托并获取返回值
            )
            .Select(
                t => bonus[t.abilityScoreType] += t.bonus           //将每个返回值解构并累加到对应属性点
            );
        return bonus;
    }

    private AbilityScores CalcAbilityScores()                       //计算属性值
    {
        AbilityScores bonus = InvokeOnCalcAbilityScores();

        return _baseAbilityScores + bonus;
    }

    public AbilityScores GetAbilityScores()                         //返回属性值
    {
        return CalcAbilityScores();
    }
}