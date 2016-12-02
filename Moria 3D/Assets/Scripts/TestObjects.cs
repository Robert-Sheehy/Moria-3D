using UnityEngine;
using System.Collections;

public class TestObjects : MonoBehaviour {


    ObjectManager theObjectManager;
	// Use this for initialization
	void Start () {
        theObjectManager = FindObjectOfType<ObjectManager>();
        if (theObjectManager) print("Found Object Manager"); else print("Not Found Object Manager");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.W))
            theObjectManager.getObjectWithIdOf(Item.list.wall);
        if (Input.GetKeyDown(KeyCode.S))
            theObjectManager.getObjectWithIdOf(Item.list.weapon);
        if (Input.GetKeyDown(KeyCode.F))
            theObjectManager.getObjectWithIdOf(Item.list.food);
        if (Input.GetKeyDown(KeyCode.A))
            theObjectManager.getObjectWithIdOf(Item.list.armor);

    }
}
