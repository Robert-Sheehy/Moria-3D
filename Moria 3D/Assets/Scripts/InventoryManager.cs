﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour {
    public GameObject text;
    int maxInventoryNumberOfItems = 20;
    int maxInventoryWeight = 20;
    int curInventoryNumWeight;

    internal List<LootableItem> inventoryItems;
    internal List<LootableItem> equipedItems;
    

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

    ObjectManager objectManager;


    // Use this for initialization
    void Start () {
        inventoryItems = new List<LootableItem>();
        equipedItems = new List<LootableItem>();

        objectManager = FindObjectOfType<ObjectManager>();


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
            Debug.Log("Equiped " + equipedItems[0].Description);
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            AddItem(objectManager.Weapons[inventoryItems.Count]);
                Debug.Log("Added " + inventoryItems[inventoryItems.Count - 1].Description);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            AddItem(objectManager.Armors[inventoryItems.Count]);
            Debug.Log("Added " + inventoryItems[inventoryItems.Count - 1].Description);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            removeItemAt(0);
            Debug.Log("Removed " + inventoryItems[0].Description);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(inventoryItems.Count + " items in inventory");
            Debug.Log(equipedItems.Count + " items equiped");
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            equipItem(inventoryItems.Count - 1);
            Debug.Log(weaponSlot.Description + " is equiped");
            Debug.Log(armorSlot.Description + " is equiped");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            objectManager.getObjectWithIdOf(Item.list.weapon);

        }

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
            equipedItems.Add(inventoryItems[itemToEquip]);
            item = equipedItems[itemToEquip];
        }

        if (item.GetType() == typeof(Weapon))
            weaponSlot = item;
        else if (item.GetType() == typeof(Armor))
            armorSlot = item;

        return item;
    }

    public void equipWeapon(int weaponToEquip)
    {
        if (weaponToEquip <= inventoryItems.Count && equipedItems.Count < inventoryItems.Count)
        {
            equipedItems.Add(inventoryItems[weaponToEquip]);
        }
        else
            Debug.Log("No items to equip");
    }

    public void equipArmor(int armorToEquip)
    {
        if (armorToEquip <= inventoryItems.Count)
        {
            equipedItems.Add(inventoryItems[armorToEquip]);
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("item"))
        {
            foreach(Weapon wpn in objectManager.Weapons)
            {
                if (wpn.Description == col.name)
                {
                    if(!inventoryFull())
                    {
                        AddItem(wpn);
                        Destroy(col.gameObject);
                        Debug.Log(wpn.Description + " added to inventory");
                    }
                    else
                    {
                        Debug.Log("Inventory is full, Can't add item");
                    }
                }
            }
        }
    }






}