using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    public Transform shield;
    public Transform sword;

    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Equipment[] defaultItems;
    public Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    public SkinnedMeshRenderer targetMesh;

    public InventoryUI inventoryUI;

    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        int numOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numOfSlots];
        currentMeshes = new SkinnedMeshRenderer[numOfSlots];
        EquipDefaultItems();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnEquipAll();
        }
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = Unequip(slotIndex);


        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, null);
        }

        SetEquipmentBlendShapes(newItem, 100);
        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        currentMeshes[slotIndex] = newMesh;

        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            newMesh.rootBone = sword;
        }
        else if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
        {
            newMesh.rootBone = shield;
        }
        else
        {
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;
        }
    }

    public Equipment Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            if(currentMeshes[slotIndex] != null)
            {

                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }

        return null;
    }

    public void UnEquipAll()
    {
    for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    public void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach(EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    public void EquipDefaultItems()
    {
        foreach(Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

}
