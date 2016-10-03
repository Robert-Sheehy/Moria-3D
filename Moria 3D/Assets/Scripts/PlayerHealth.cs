using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;                                 //players starting health
    public int currentHealth;                                   //player's current health
    public Slider healthSlider;                                 // reference to playr's health bar
    public Image damageImage;                                   //screen reaction to taking damage
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);




    CharacterController playerMovement;                         //reference player movemnt
    bool isDead;                                                //if player is dead
    bool damaged;                                               //if player is damaged

    // Use this for initialization
    void Start()
    {



        playerMovement = GetComponent<CharacterController>();
        currentHealth = maxHealth;          //set starting health
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
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {

            //Death();
        }


    }

    // void Death {

    //  isDead = true;
    // playerMovement.enabled = false;



    // }


}
