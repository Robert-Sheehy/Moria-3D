using UnityEngine;
using System.Collections;
using System;

public class CharacterCombat : MonoBehaviour {
    internal int weaponDamage;
    internal int critcalBonus;
    internal int weaponDamageBonus;
    internal int itemBonusDamage;
    internal int strBonusDamage;
    internal int weaponHitBonus;
    internal int itemHitBonus;
    internal int fightingAbility;
    internal int weightOfWeapon;
    internal int fightingAbilityAt0;
    internal int hitChance;
    internal int DEXHitBonus;
    internal int STRHitBonus;
    private int enemyHealth;
    private int playerHealth;
    public GameObject player;
    public GameObject enemy;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        

	}

    internal void ApplyDamage(int totalDamage)
    {
        
            
        enemyHealth += enemyHealth - totalDamage;
    }

    internal void applyHit(int hitChance)
    {
        throw new NotImplementedException();
    }

    internal void applyCrit(int critChance)
    {
        throw new NotImplementedException();
    }
}
