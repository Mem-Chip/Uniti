using System;

public enum AbilityType
{
    Strength,
    Dexterity,
    Constitution,
    Intelligence,
    Wisdom,
    Charisma
}

public struct Ability
{
    public int strength, dexterity, constitution, intelligence, wisdom, charisma;

    public Ability(int strength = 0, int dexterity = 0, int constitution = 0, int intelligence = 0, int wisdom = 0, int charisma = 0)
    {
        this.strength = strength;
        this.dexterity = dexterity;
        this.constitution = constitution;
        this.intelligence = intelligence;
        this.wisdom = wisdom;
        this.charisma = charisma;
    }

    public int this[AbilityType type]           //提供根据属性类型的访问方法
    {
        get => type switch
        {
            AbilityType.Strength => strength,
            AbilityType.Dexterity => dexterity,
            AbilityType.Constitution => constitution,
            AbilityType.Intelligence => intelligence,
            AbilityType.Wisdom => wisdom,
            AbilityType.Charisma => charisma,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        set
        {
            switch (type)
            {
                case AbilityType.Strength: strength = value; break;
                case AbilityType.Dexterity: dexterity = value; break;
                case AbilityType.Constitution: constitution = value; break;
                case AbilityType.Intelligence: intelligence = value; break;
                case AbilityType.Wisdom: wisdom = value; break;
                case AbilityType.Charisma: charisma = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public static Ability operator +(Ability left, Ability right)   //相加
    {
        return new Ability(
            left.strength + right.strength,
            left.dexterity + right.dexterity,
            left.constitution + right.constitution,
            left.intelligence + right.intelligence,
            left.wisdom + right.wisdom,
            left.charisma + right.charisma);
    }
}
