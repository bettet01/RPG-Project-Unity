using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Image icon;
    public Button removeButton;
    public Button useButton;
    public Image countImage;
    public Text countText;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        useButton.interactable = true;
    }

    public void ClearSlot()
    {
        this.item = null;
        icon.sprite = null;
        icon.enabled = false;
        countImage.enabled = false;
        countText.enabled = false;
        removeButton.interactable = false;
        useButton.interactable = false;
    }

    public void onRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void ShowItemCount(Item item)
    {
        if(item.itemCount < 2)
        {
            countImage.enabled = false;
            countText.enabled = false;

        } else
        {
            countImage.enabled = true;
            countText.enabled = true;
            countText.text = item.itemCount.ToString();
        }
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
