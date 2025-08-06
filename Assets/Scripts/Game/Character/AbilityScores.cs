using System;

public enum AbilityScoreType
{
    Strength,
    Dexterity,
    Constitution,
    Intelligence,
    Wisdom,
    Charisma
}

public struct AbilityScores
{
    public int strength, dexterity, constitution, intelligence, wisdom, charisma;

    public AbilityScores(int strength = 0, int dexterity = 0, int constitution = 0, int intelligence = 0, int wisdom = 0, int charisma = 0)
    {
        this.strength = strength;
        this.dexterity = dexterity;
        this.constitution = constitution;
        this.intelligence = intelligence;
        this.wisdom = wisdom;
        this.charisma = charisma;
    }

    public int this[AbilityScoreType type]           //提供根据属性类型的访问方法
    {
        get => type switch
        {
            AbilityScoreType.Strength => strength,
            AbilityScoreType.Dexterity => dexterity,
            AbilityScoreType.Constitution => constitution,
            AbilityScoreType.Intelligence => intelligence,
            AbilityScoreType.Wisdom => wisdom,
            AbilityScoreType.Charisma => charisma,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        set
        {
            switch (type)
            {
                case AbilityScoreType.Strength: strength = value; break;
                case AbilityScoreType.Dexterity: dexterity = value; break;
                case AbilityScoreType.Constitution: constitution = value; break;
                case AbilityScoreType.Intelligence: intelligence = value; break;
                case AbilityScoreType.Wisdom: wisdom = value; break;
                case AbilityScoreType.Charisma: charisma = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public static AbilityScores operator +(AbilityScores left, AbilityScores right)   //相加
    {
        return new AbilityScores(
            left.strength + right.strength,
            left.dexterity + right.dexterity,
            left.constitution + right.constitution,
            left.intelligence + right.intelligence,
            left.wisdom + right.wisdom,
            left.charisma + right.charisma);
    }
}
