using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;                                 //players starting HP
    public int maxMP = 100;                                   //players starting MP
    public int currentHP;                                   //player's current health
    public int currentMP;                                   //player's current health
    public Slider HP_Slider;                                    //references player HP bar
    public Slider MP_Slider;                                    // reference to playr's MP bar
    public Image damageImage;                                   //screen reaction to taking damage
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);




    CharacterController playerMovement;                         //reference player movemnt
    bool isDead = false;                                                //if player is dead
    bool OutOfMana = false;
    bool damaged;                                               //if player is damaged
    bool cast;                                                  //if player casts                                                         

    // Use this for initialization
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
    }


    public void TakeDamage(int amount)
    {

        damaged = true;
        currentHP -= amount;
        HP_Slider.value = currentHP;

        if (currentHP <= 0 && !isDead)
        {

            //Death(); 
        }
        if (currentMP <= 0 && !OutOfMana)
        {

            //Death(); 
        }

    }
    public void UseMagic(int amount)
    {

        damaged = true;
        currentMP -= amount;
        HP_Slider.value = currentHP;

        if (currentHP <= 0 && !isDead)
        {

            //Death(); 
        }
        if (currentMP <= 0 && !isDead)
        {

            //Death(); 
        }

    }


    //void Death {

    //isDead = false;
    //playerMovement.enabled = false;



}



