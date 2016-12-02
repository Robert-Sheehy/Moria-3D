using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100.0f;                                   //players starting HP
    public float maxMP = 100.0f;                                   //players starting MP
    public float currentHP;                                     //player's current HP
    public float currentMP;                                     //player's current MP
    public int HPregen = 5;                                       //player's current HP Regen rate  
    public int MPregen = 3;                                       //player's current MP Regen rate  
    public Slider HP_Slider;                                  //references player HP bar
    public Slider MP_Slider;                                  // reference to playr's MP bar
    public Image damageImage;                                 //screen reaction to taking damage
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);




    CharacterController playerMovement;                         //reference player movemnt
    bool isDead = false;                                        //if player is dead
    bool OutOfMana = false;
    bool damaged;                                               //if player is damaged
    bool cast;                                                  //if player casts 
    bool RegenHP = false;                                       //if Health is regening
    bool RegenMP = false;                                       //if Mana is regening       

    // Use this for initialization
    void awake()
    {
        currentHP = maxHP;
        currentMP = maxMP;

    }



    void Start()
    {



        playerMovement = GetComponent<CharacterController>();
        currentHP = maxHP;          //set starting health
        currentMP = maxMP;
    }

    // Update is called once per frame
    void Update()
    {

        if (damaged)
        {

            damageImage.color = flashColour;

        }
        else
        {

            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

        }

        damaged = false;

        if (cast)
        {


        }
        while (currentHP < maxHP && !isDead)
        {
            RegenHP = true;
        }
        while (currentMP < maxMP && !OutOfMana)
        {
            RegenMP = true;
        }

    }




    public void TakeDamage(int amount)
    {

        damaged = true;
        currentHP -= amount;
        HP_Slider.value = currentHP;

        if (currentHP <= 0 && !isDead && currentMP <= 0 && !OutOfMana)
        {

            isDead = true;
            OutOfMana = true;
        }


     

    }
    public void UseMagic(int amount)
    {

        cast = true;
        currentMP -= amount;
        MP_Slider.value = currentMP;

        if (currentMP <= 0 && !OutOfMana)
        {
            isDead = true;
            //playerMovement.enabled = false;
        }
        if (currentMP <= 0 && OutOfMana)
        {

            //RegenMP start 
        }

    }


    //void Death {

    //isDead = false;
    //playerMovement.enabled = false;



}

