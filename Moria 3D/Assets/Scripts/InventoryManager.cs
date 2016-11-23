using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour {
    int maxInventoryWeight = 20;
    int curInventoryNumWeight;
    List<LootableItem> inventoryItems;
    List<LootableItem> equipeditems;
    public Text text;
    private int maxInventoryNumberOfItems = 20;

    LootableItem weaponSlot;
    /*LootableItem mainWeaponSlot;
    LootableItem secondaryWeaponSlot;*/
    LootableItem armorSlot;
    /*LootableItem helmetSlot;
    LootableItem chestplateSlot;
    LootableItem gaunletSlot;
    LootableItem bootSlot;*/
    LootableItem ringSlot;
    LootableItem amuletSlot;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        /*if (inventoryFull())
            text.enabled = true;*/
	}

    public LootableItem removeItemAt(int index)
    {

        LootableItem item = null;
        if (index < inventoryItems.Count)

        {
            item = inventoryItems[index];
            inventoryItems.RemoveAt(index);
        }
        return item; 
    }

    public bool AddItem(LootableItem itemToAdd)
    {
        if (inventoryFull()) return false;

        inventoryItems.Add(itemToAdd);
        return true;

    }

    private bool inventoryFull()
    {
        return inventoryItems.Count >= maxInventoryNumberOfItems;
    }

    public LootableItem equipItem(int itemToEquip)
    {
        LootableItem item = null;

        if(itemToEquip < inventoryItems.Count)
        {
            equipeditems.Add(inventoryItems[itemToEquip]);
            item = equipeditems[itemToEquip];
        }

        if (item.GetType() == typeof(Weapon))
            weaponSlot = item;
        else if (item.GetType() == typeof(Armor))
            armorSlot = item;

        return item;
    }
}
