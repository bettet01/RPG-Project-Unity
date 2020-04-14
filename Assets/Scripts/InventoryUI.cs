using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    EquipmentManager equipmentManager;
    public GameObject inventoryUI;
    public GameObject CharacterInfoUI;
    public Text[] values;

    // current equipment inventory slots
    public EquippedSlot[] equippedSlots;

    public Transform itemsParent;
    InventorySlot[] slots;

    // Coin Items
    public Text CoinText;
    public PlayerStats player;


    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        equipmentManager = EquipmentManager.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        equipmentManager.onEquipmentChanged += UpdateEquipment;
        slots = GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
        if (Input.GetButtonDown("Inventory"))
        {
            
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CharacterInfoUI.SetActive(!CharacterInfoUI.activeSelf);
        }
    }

    void UpdateStats()
    {
        CoinText.text = player.coins.ToString();
        values[0].text = player.level.ToString();
        values[1].text = player.currentExp.ToString() + " / " + player.maxExp.ToString();
        values[2].text = player.strength.GetValue().ToString();
        values[3].text = player.defense.GetValue().ToString();
        values[4].text = player.dexterity.GetValue().ToString();
        values[5].text = player.intelligence.GetValue().ToString();

    }

    void UpdateUI()
    {
        Debug.Log("UPDATING UI");
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].ShowItemCount(inventory.items[i]);
                slots[i].AddItem(inventory.items[i]);

            } else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void UpdateEquipment(Equipment newItem, Equipment oldItem)
    {
        if (oldItem != null)
        {
            int slotIndex = (int)oldItem.equipSlot;
            equippedSlots[slotIndex].ClearSlot();
        }
        if (newItem != null && newItem.isDefaultItem == false)
        {
            int slotIndex = (int)newItem.equipSlot;
            equippedSlots[slotIndex].AddItem(newItem);
        } 
    }
}
