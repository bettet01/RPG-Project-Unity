using System;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public int itemCount = 1;
    public Boolean isDefaultItem = false;
    public int cost;

    public virtual void Use()
    {
        // Use The Item
        Debug.Log("Using " + name);

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
