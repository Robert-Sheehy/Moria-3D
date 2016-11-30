using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour {
    int maxInventoryWeight = 20;
    int curInventoryNumWeight;
    List<LootableItem> inventoryItems;
    internal List<LootableItem> equipeditems;
    public GameObject text;
    int maxInventoryNumberOfItems = 20;

    internal LootableItem weaponSlot;
    internal LootableItem mainWeaponSlot;
    internal LootableItem secondaryWeaponSlot;
    internal LootableItem armorSlot;
    internal LootableItem helmetSlot;
    internal LootableItem chestplateSlot;
    internal LootableItem gaunletSlot;
    internal LootableItem bootSlot;
    internal LootableItem ringSlot;
    internal LootableItem amuletSlot;



    // Use this for initialization
    void Start () {
        inventoryItems = new List<LootableItem>();
        equipeditems = new List<LootableItem>();

        /*foreach (LootableItem LI in this.GetComponent<ObjectManager>().Weapons)
        {
            AddItem(LI);
        }*/

        for (int i = 0; i < inventoryItems.Count; i++)
            Debug.Log(inventoryItems[i].Description);
    }
	
	// Update is called once per frame
	void Update () {
        if (inventoryFull())
            text.SetActive(true);
        else
            text.SetActive(false);

        if(Input.GetKeyDown(KeyCode.E))
        {
            equipWeapon(0);
            Debug.Log("Equiped " + equipeditems[0].Description);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            AddItem(GetComponent<ObjectManager>().Weapons[0]);
                Debug.Log("Added " + inventoryItems[0].Description);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            removeItemAt(0);
                Debug.Log("Removed " + inventoryItems[0].Description);
        }

        if (Input.GetKeyDown(KeyCode.I))
            Debug.Log(inventoryItems.Count + " in inventory");
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
        if (inventoryFull())
            return false;
        else
        {
            inventoryItems.Add(itemToAdd);
            return true;
        }

    }

    private bool inventoryFull()
    {
        if (inventoryItems.Count >= maxInventoryNumberOfItems)
            return true;
        else
            return false;
    }

    public LootableItem equipItem(int itemToEquip)
    {

        LootableItem item = null;

        if(itemToEquip <= inventoryItems.Count)
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

    public void equipWeapon(int weaponToEquip)
    {
        if (weaponToEquip <= inventoryItems.Count)
        {
            equipeditems.Add(inventoryItems[weaponToEquip]);
        }
    }
}
