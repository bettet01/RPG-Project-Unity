using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            strength.AddModifier(newItem.strengthModifier);
            defense.AddModifier(newItem.defenseModifier);
            dexterity.AddModifier(newItem.dexterityModifier);
            intelligence.AddModifier(newItem.intelligenceModifier);
        }
        if (oldItem != null)
        {
            strength.RemoveModifier(oldItem.strengthModifier);
            defense.RemoveModifier(oldItem.defenseModifier);
            dexterity.RemoveModifier(oldItem.dexterityModifier);
            intelligence.RemoveModifier(oldItem.intelligenceModifier);
        }
    }

    public void addExp(int winExp)
    {
        currentExp += winExp;

        if(currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level += 1;
        maxExp = Mathf.RoundToInt(maxExp * 1.35f);
        currentExp = 0;
        //TODO: code to run when you level up
    }

    public override void Die()
    {
        base.Die();
        // kill player
        PlayerManager.instance.KillPlayer();
    }
}
