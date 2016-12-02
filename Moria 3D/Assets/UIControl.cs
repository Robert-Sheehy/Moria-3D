using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControl : MonoBehaviour {


    Text[] textBoxes;
    CharacterCombat playerDetails;

	// Use this for initialization
	void Start () {
        playerDetails = FindObjectOfType<CharacterCombat>();
        if (playerDetails) print("UI has found player"); else print("UI has not  found player");
        textBoxes =  FindObjectsOfType<Text>();
        print("Found Texts " + textBoxes.Length);

      

    }
	
	// Update is called once per frame
	void Update () {
        textBoxes[4].text = "CurrentHP : " + playerDetails.health.ToString();
        textBoxes[7].text = "CurrentMP : " + playerDetails.mana.ToString();
        textBoxes[2].text = "Constitution : " + playerDetails.constitution.ToString();
        textBoxes[7].text = "Strenght : " + playerDetails.strenght.ToString();
        textBoxes[0].text = "Dexterity : " + playerDetails.dexterty.ToString();
        textBoxes[5].text = "Intelegence : " + playerDetails.itellegence.ToString();
        textBoxes[1].text = "Wisdom : " + playerDetails.wisdom.ToString();
        textBoxes[3].text = "Charisma " + playerDetails.charisma.ToString();

    }
}
