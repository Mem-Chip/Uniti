using System;

public sealed class AbilityScoresBonusEffect : Effect
{
    private readonly AbilityType _abilityScoreType;
    private readonly int _bonus;
    private readonly Func<(AbilityType _abilityScoreType, int _bonus)> _effectFunc;

    public AbilityScoresBonusEffect((AbilityType abilityScoreType, int bonus) data, string name, Entity target, Entity effector = null, int duration = -2) : base(name, target, effector, duration)
    {
        _abilityScoreType = data.abilityScoreType;
        _bonus = data.bonus;
        _effectFunc = () => (_abilityScoreType, _bonus);
    }

    protected override void CustomEnEffect()
    {
        Target.Data.character.OnCalcAbilityScores += _effectFunc.Invoke;
        Target.OnTurnEnd += HandleEffect;
    }
    
    protected override void CustomDeEffect()
    {
        Target.Data.character.OnCalcAbilityScores -= _effectFunc.Invoke;
    }
}