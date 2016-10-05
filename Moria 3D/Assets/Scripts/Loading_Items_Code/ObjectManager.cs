using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour {


    List<Weapon> Weapons;
    List<Armor> Armors;
    List<Food> Foods;


    // Use this for initialization
    void Start () {

        loadAllItems();
	
	}

    private void loadAllItems()
    {
        loadAllWeapons();
        loadAllArmor();
        loadAllFood();
   
    }

    private void loadAllWeapons()
    {
        Weapons = new List<Weapon>();
        Weapons.Add(new Weapon("Broken Sword",1,1,7.5f,0,24,-2,-2));
        Weapons.Add(new Weapon("Dagger (Misericorde)", 1, 1, 1.5f, 0, 10, 0, 0));
        Weapons.Add(new Weapon("Dagger (Stiletto)", 1, 1, 1.2f, 0, 10, 0, 0));
        Weapons.Add(new Weapon("Hands", 1, 2, 0f, 0, 0, -2, -2));
        Weapons.Add(new Weapon("Wooden Club", 1, 3, 10f, 0, 10, 0, 0));
        Weapons.Add(new Weapon("Cat-o'-Nine Tails", 1, 4, 4f, 3, 14, 0, 0));
        Weapons.Add(new Weapon("Javelin", 1, 4, 3.0f, 3, 18, 0,0));
        Weapons.Add(new Weapon("Rapier", 1, 6, 4.0f, 4, 18, 0, 0));
        Weapons.Add(new Weapon("Sabre", 1, 7, 5.0f, 5, 50, 0, 0));
        Weapons.Add(new Weapon("Small Sword", 1, 6, 7.5f,5,48, 0, 0));
        Weapons.Add(new Weapon("Spear ", 1, 6, 5f, 5, 36, 0, 0));
        Weapons.Add(new Weapon("War Hammer", 3, 3, 12.0f, 5, 225, 0, 0));
        Weapons.Add(new Weapon("Mace", 2, 4, 12.0f, 6, 130, 0, 0));
        Weapons.Add(new Weapon("Backsword", 1, 9, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Cutlass", 1, 7, 11.0f, 7, 85, 0, 0));
        Weapons.Add(new Weapon("Broadsword", 2,5, 15.0f, 9, 225, 0, 0));
        Weapons.Add(new Weapon("Morningstar", 2, 6, 15.0f, 9, 396, 0, 0));


        Weapons.Add(new Weapon("Lance", 2, 8, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Flail", 2, 6, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Longsword", 1, 10, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Battle Axe", 3, 4, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Beaked Axe", 2, 6, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Bastard Sword", 3, 4, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Pike", 2, 5, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Katana", 3, 4, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Ball and Chain", 2,4, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Glaive", 2, 6, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Halberd", 3, 4, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Two-Handed Sword (Claymore)", 3, 6, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Executioner's Sword", 4, 5, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Two-Handed Sword (Zweihander)", 4, 6, 1.5f, 0, 0, 0, 0));
        Weapons.Add(new Weapon("Two-Handed Sword (Flamberge)", 4, 5, 1.5f, 0, 0, 0, 0));



    }
    private void loadAllArmor()
    {
        Armors = new List<Armor>();
        Armors.Add(new Armor("Robe", 1, 2.0f, 2, 4));
        Armors.Add(new Armor("Soft Leather Armor", 1, 8.0f, 4, 18));
        Armors.Add(new Armor("Soft leather Ring Mail", 10,13.0f,6,160));
        Armors.Add(new Armor("Chain Mail",26,22.0f,14,530));
        Armors.Add(new Armor("Bar Chain Mail",34,28.0f,18,720));
        Armors.Add(new Armor("Laminated Armor",38,30.0f,20,825));
        Armors.Add(new Armor("Ribbed Plate Armor",50,38.0f,28,1200));
    }

    private void loadAllFood(){

        Foods = new List<Food>;
        Foods.Add(new Food("Pint Of Fine Wine", 400, 0, 2));
        Foods.Add(new Food("Pint of Fine Ale",500,0,2));
        Foods.Add(new Food("Hard biscuit",500,0,1));
        Foods.Add(new Food("Strip Of Beef Jerky",1750,0,4));
        Foods.Add(new Food("Ration Of Food", 5000, 0, 3));

    }

    // Update is called once per frame
    void Update () {
	
	               }
}
