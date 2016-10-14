using UnityEngine;
using System.Collections;
using System;

public class CharacterCombat : MonoBehaviour  {
    public int weaponDamage;
    public int critcalBonus;
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

    private int health;


    // Use this for initialization
    void Start () {
	
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

}
