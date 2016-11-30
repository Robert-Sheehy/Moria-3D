using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControl : MonoBehaviour {


    Text[] textBoxes;
    CharacterCombat playerDetails;

	// Use this for initialization
	void Start () {
        //playerDetails = GetComponent<CharacterCombat>();
        //playerDetails.
        textBoxes =  FindObjectsOfType<Text>();
        print("Found Texts " + textBoxes.Length);

        string hold = textBoxes[0].text = "Constitution : " + playerDetails.constitution.ToString();
        //string hold = textBoxes[1].text = "HI : " + playerDetails.constitution.ToString();
       // string hold = textBoxes[0].text = "Constitution : " + playerDetails.constitution.ToString();
       // string hold = textBoxes[0].text = "Constitution : " + playerDetails.constitution.ToString();
       // string hold = textBoxes[0].text = "Constitution : " + playerDetails.constitution.ToString();
       // string hold = textBoxes[0].text = "Constitution : " + playerDetails.constitution.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
