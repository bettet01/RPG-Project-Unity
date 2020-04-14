using UnityEngine;
using UnityEngine.UI;

public class EquippedSlot : MonoBehaviour
{
    public Inventory inventory;
    public EquipmentManager equipmentManager;
    public Equipment item;
    public Image icon;
    public Button useButton;

    private void Start()
    {
        inventory = Inventory.instance;
        equipmentManager = EquipmentManager.instance;
        ClearSlot();
    }

    public void AddItem(Equipment newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        useButton.interactable = true;
    }

    public void ClearSlot()
    {
        this.item = null;
        icon.sprite = null;
        icon.enabled = false;
        useButton.interactable = false;
    }

    public void UseItem()
    {
        if (item != null)
        {
            int slotIndex = (int)item.equipSlot;
            EquipmentManager.instance.Unequip(slotIndex);

            EquipmentManager.instance.Equip(equipmentManager.defaultItems[slotIndex]);
        }
    }
}
