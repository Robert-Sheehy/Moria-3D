using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour {
    int maxInventoryWeight = 20;
    int curInventoryNumWeight;
    List<LootableItem> inventoryItems;
    public Text text;
    private int maxInventoryNumberOfItems = 20;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        /*inventoryItems = new List<ObjectManager>();
        


        if(inventoryItems.Add(new ObjectManager))
        {
            curInventoryNum++;
        }

        if(curInventoryNum >= maxInventoryNum)
        {
            text.setActive(true);
        }
        else 
        {
            text.setActive(false);
        }*/
	
	}
    LootableItem removeItemAt(int index)
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
        return inventoryItems.Count == maxInventoryNumberOfItems;
    }

    public bool equipitem()
    {
        return false;
    }
}
