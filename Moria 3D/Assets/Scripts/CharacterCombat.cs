using UnityEngine;
using System.Collections;
using System;

public class CharacterCombat : MonoBehaviour  {
    public int weaponDamage;
    public int weaponDamageBonus;
    public int itemBonusDamage;
    public int strBonusDamage;//strenght bonus damage
    public int weaponHitBonus;
    public int itemHitBonus;
    public int fightingAbility;
    public int weightOfWeapon;
    public int fightingAbilityAt0;
    public int hitChance;
    public int DEXHitBonus;
    public int STRHitBonus; //strenght hit bonus

    public int AC;


    internal int constitution;


    public int health;


    InventoryManager myINventory;

    // Use this for initialization
    void Start () {
        myINventory = FindObjectOfType<InventoryManager>();
       // Weapon w = (Weapon) myINventory.weaponSlot;
       // weaponDamage = w.Damage;
	}
	
	// Update is called once per frame
	void Update () {

        
	}

    internal void ApplyDamage(int totalDamage)
    {
        health -= totalDamage;

        if (health <= 0)
            Kill();

        //if (weapon.tag == "Holy Avenger")
        //{
        //    health += health - totalDamage;
        //}

        //else if(weapon.tag == "Defender")
        //{
        //    health += health - totalDamage;
        //}

        //else if (weapon.tag == "Slay Animal")
        //{
        //    health += health - totalDamage;
        //}

        //else if (weapon.tag == "Slay Dragon")
        //{
        //    health += health - totalDamage;
        //}

        //else if (weapon.tag == "Slay Evil")
        //{
        //    health += health - totalDamage;
        //}

        //else if (weapon.tag == "Slay Undead")
        //{
        //    health += health - totalDamage;
        //}

        //else if (weapon.tag == "Flame Tongue")
        //{
        //    health += health - totalDamage;
        //}

        //else if (weapon.tag == "Frost Brand")
        //{
        //    health += health - totalDamage;
        //}
    }

    private void Kill()
    {

    }

    public void Values()
    {
        Weapon w = (Weapon)myINventory.weaponSlot;
        weaponDamage = w.Damage;

        weaponDamageBonus = w.DamBuff;

        weaponHitBonus = w.HitBuff;

        weightOfWeapon = (int)w.Weight;

        Ring a =(Ring) myINventory.ringSlot;
        itemBonusDamage += a.Damage;
        itemHitBonus += a.Hit;

        Armor b = (Armor)myINventory.armorSlot;
        AC += b.AC;

        Armor c = (Armor)myINventory.helmetSlot;
        AC += c.AC;

        Armor d = (Armor)myINventory.chestplateSlot;
        AC += d.AC;

        Armor e = (Armor)myINventory.gaunletSlot;
        AC += e.AC;

        Armor f = (Armor)myINventory.bootSlot;
        AC += f.AC;
    }

}
