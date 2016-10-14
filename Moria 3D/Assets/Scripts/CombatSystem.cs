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


        int  totalDamage = (attacker.weaponDamage  * attacker.critcalBonus) + attacker.weaponDamageBonus + attacker.itemBonusDamage + attacker.strBonusDamage;
        
       
            victim.ApplyDamage(totalDamage);
            print("Damaged at" + totalDamage);
       
       
          
    }

    public void Hit(CharacterCombat attacker, CharacterCombat victim)
    {

        randomHitNumber = Random.Range(1, 20);
        int hitChance = (attacker.weaponHitBonus + attacker.itemHitBonus + attacker.DEXHitBonus + attacker.STRHitBonus) * 3 + attacker.fightingAbility;
        randomHitNumber2 = Random.Range(1, hitChance);

        if (randomHitNumber == 1)
        {
            print("you missed");
            
            
        }
        else if ((randomHitNumber == 20) || (randomHitNumber2 > monsterAC))
        {
            Attack(attacker, victim);
            
        }
    }

    public int CritialHitBonus(CharacterCombat attacker, CharacterCombat victim)
    {
        int randomCritNumber = Random.Range(1, 5000);
        if (randomCritNumber < criticalBonus)
        {
            return (attacker.weaponHitBonus + attacker.itemHitBonus + attacker.DEXHitBonus + attacker.STRHitBonus) * 5 + attacker.fightingAbility + (attacker.weightOfWeapon) * 10 - attacker.fightingAbilityAt0;
        }
        else
            print("denied");
         return 1;
    }
}
