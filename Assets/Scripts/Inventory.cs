using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int space = 20;
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one instance of Inventory");
            return;
        } 
            instance = this;
    }

    #endregion


    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    

    public List<Item> items = new List<Item>();

    public bool Add(Item item) {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not Enough Space");
                return false;
            }
            for(int i = 0; i < items.Count; i++)
            {
                if(item.name == items[i].name)
                {
                    items[i].itemCount++;
                    onItemChangedCallBack.Invoke();
                    return true;
                }
            }
            items.Add(item);

            onItemChangedCallBack.Invoke();

        }
        return true;
    }

    public void Remove(Item item)
    {
        if(item.itemCount > 1)
        {
            DecreaseCount(item);
        }
        else
        {
            items.Remove(item);
            
        }

        onItemChangedCallBack.Invoke();
    }

    public void DecreaseCount(Item item)
    {
        item.itemCount--;
    }
}
