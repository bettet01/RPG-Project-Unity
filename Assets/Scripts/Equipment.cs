using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public EquipmentMeshRegion[] coveredMeshRegions;
    public SkinnedMeshRenderer mesh;
    public Modifier healthModifier;
    public Modifier manaModifier;
    public Modifier strengthModifier;
    public Modifier defenseModifier;
    public Modifier dexterityModifier;
    public Modifier intelligenceModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet};

public enum EquipmentMeshRegion { Legs, Arms, Torso}; // attached to body blend shapes