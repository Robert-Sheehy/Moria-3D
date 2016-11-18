using UnityEngine;
using System.Collections;

public class CombatSystem : MonoBehaviour {
    private int randomHitNumber;
    private int monsterAC;
    private int randomHitNumber2;
    private int hitChance;
    private int criticalBonus;
    
 
  
    



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}



    public void Attack(CharacterCombat attacker,CharacterCombat victim)
    {



        if (Hit(attacker, victim))
        {
            int totalDamage = (attacker.weaponDamage * CritialHitBonus( attacker,victim  ) )+ attacker.weaponDamageBonus + attacker.itemBonusDamage + attacker.strBonusDamage;

            victim.ApplyDamage(totalDamage);
            print("Damaged at " + totalDamage);
        }
        
       
          
    }

    public bool Hit(CharacterCombat attacker, CharacterCombat victim)
    {

        randomHitNumber = Random.Range(1, 20);
        print(randomHitNumber);
        int hitChance = (attacker.weaponHitBonus + attacker.itemHitBonus + attacker.DEXHitBonus + attacker.STRHitBonus) * 3 + attacker.fightingAbility;
        randomHitNumber2 = Random.Range(1, hitChance);
        print(randomHitNumber2);
        if (randomHitNumber == 1)
        {
      
            return false;
            
            
        }
        else if ((randomHitNumber == 20) || (randomHitNumber2 > victim.AC))
        {
            return true;
            
        }
        return false;

    }

    public int CritialHitBonus(CharacterCombat attacker, CharacterCombat victim)
    {
        
        int randomCritNumber = Random.Range(1, 5000);
        print("crit number " +randomCritNumber);
        int criticalBonus= (attacker.weaponHitBonus + attacker.itemHitBonus + attacker.DEXHitBonus + attacker.STRHitBonus) * 5 + attacker.fightingAbility + (attacker.weightOfWeapon) * 10 - attacker.fightingAbilityAt0;
        if (randomCritNumber < criticalBonus)
        {
            print("Critical ");

            return criticalBonus;
        }
        
        
            print("denied");
           return 1;
        
        
    }
}
