using UnityEngine;
using System.Collections;

public class TestCombat : MonoBehaviour {
    public CharacterCombat A, B;
    CombatSystem combat;
	// Use this for initialization
	void Start () {
        combat = FindObjectOfType<CombatSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A)) combat.Attack(A, B);
        if (Input.GetKeyDown(KeyCode.B)) combat.Attack(B, A);


    }
}
