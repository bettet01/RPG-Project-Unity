using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;
    [SerializeField]
    private List<int> modifiers = new List<int>();


    public void AddModifier(Modifier modifier)
    {
        if (modifier.modifierNumber != 0)
        {
            if(modifier.modifierType == ModifierType.Add)
            {
                modifiers.Add((int) modifier.modifierNumber);
            } else if(modifier.modifierType == ModifierType.Multiply)
            {
                modifiers.Add(Mathf.RoundToInt(baseValue * modifier.modifierNumber));
            }
            
        }
    }

    public void RemoveModifier(Modifier modifier)
    {
        if (modifier.modifierNumber != 0)
        {
            if (modifier.modifierType == ModifierType.Add)
            {
                modifiers.Remove((int)modifier.modifierNumber);
            }
            else if (modifier.modifierType == ModifierType.Multiply)
            {
                modifiers.Remove(Mathf.RoundToInt(baseValue * modifier.modifierNumber));
            }
        }
    }

    public int GetBaseValue()
    {
        return baseValue;
    }

    public int GetValue()
    {
        int finalValue = baseValue;

        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void SetValue(int value)
    {
        baseValue = value;
    }
}
