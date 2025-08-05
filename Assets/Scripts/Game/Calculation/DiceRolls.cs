using JetBrains.Annotations;
using UnityEngine;

public static class DiceRolls
{
    public static int RollDice(int sides, int rolls)
    {
        int total = 0;
        for (int i = 0; i < rolls; i++)
        {
            total += Random.Range(1, sides + 1);
        }
        return total;
    }
    // Rolls dice and keeps the highest or lowest results based on the direction parameter
    // If direction is true, keeps the highest results; if false, keeps the lowest results
    public static int RollDiceandKeep(int sides, int rolls, int keep, bool direction)
    {
        if (keep > rolls)
        {
            throw new System.ArgumentException("Keep cannot be greater than rolls.");
        }

        int[] results = new int[rolls];
        for (int i = 0; i < rolls; i++)
        {
            results[i] = Random.Range(1, sides + 1);
        }

        System.Array.Sort(results);
        int total = 0;
        if (direction) for (int i = rolls - keep; i < rolls; i++)
            {
                total += results[i];
            }
        else for (int i = 0; i < keep; i++)
            {
                total += results[i];
            }
        return total;
    }
    // Rolls a D20 with modifiers and status effects
    // Status can be 0 (normal), 1 (advantage), or 2 (disadvantage)
    public static int D20(int modifiers = 0, int status = 0)
    {
        int roll = Random.Range(1, 21);
        int roll1 = Random.Range(1, 21);
        switch (status)
        {
            case 0: // Normal roll
                return roll + modifiers;
            case 1: // Advantage
                return Mathf.Max(roll, roll1) + modifiers;
            case 2: // Disadvantage
                return Mathf.Min(roll, roll1) + modifiers;
            default:
                throw new System.ArgumentException("Invalid status for D20 roll.");
        }
    }
}