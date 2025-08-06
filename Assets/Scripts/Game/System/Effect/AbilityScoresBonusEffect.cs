using System;

public sealed class AbilityScoresBonusEffect : Effect
{
    private readonly AbilityScoreType _abilityScoreType;
    private readonly int _bonus;
    private readonly Func<(AbilityScoreType _abilityScoreType, int _bonus)> _effectFunc;

    public AbilityScoresBonusEffect((AbilityScoreType abilityScoreType, int bonus) data, string name, Entity target, Entity effector, int duration = -2) : base(name, target, effector, duration)
    {
        _abilityScoreType = data.abilityScoreType;
        _bonus = data.bonus;
        _effectFunc = () => (_abilityScoreType, _bonus);
    }

    public override void EnEffect()
    {
        Target.Data.character.OnCalcAbilityScores += _effectFunc.Invoke;
        Target.OnTurnEnd += HandleEffect;
    }
    
    public override void DeEffect()
    {
        Target.Data.character.OnCalcAbilityScores -= _effectFunc.Invoke;
    }
}