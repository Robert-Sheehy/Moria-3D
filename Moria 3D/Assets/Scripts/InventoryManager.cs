using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour {
    int maxInventoryWeight = 20;
    int curInventoryNumWeight;
    List<LootableItem> inventoryItems;
    List<LootableItem> equipedItems;
    public GameObject text;
    int maxInventoryNumberOfItems = 20;
   /// h
    LootableItem weaponSlot;
    /*LootableItem mainWeaponSlot;
    LootableItem secondaryWeaponSlot;*/
    LootableItem armorSlot;
    /*LootableItem helmetSlot;
    LootableItem chestplateSlot;
    LootableItem gaunletSlot;
    LootableItem bootSlot;
    LootableItem ringSlot;
    LootableItem amuletSlot;*/



    // Use this for initialization
    void Start () {
        inventoryItems = new List<LootableItem>();
        equipedItems = new List<LootableItem>();

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

        if(Input.GetKeyDown(KeyCode.A))
        {
            AddItem(GetComponent<ObjectManager>().Weapons[inventoryItems.Count]);
                Debug.Log("Added " + inventoryItems[inventoryItems.Count - 1].Description);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddItem(GetComponent<ObjectManager>().Armors[inventoryItems.Count]);
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
            foreach(Weapon wpn in GetComponent<ObjectManager>().Weapons)
            {
                if (wpn.Description == col.name)
                {
                    AddItem(wpn);
                    Debug.Log(wpn.Description + " added to inventory");
                }
            }
            Destroy(col.gameObject);
        }
    }
}