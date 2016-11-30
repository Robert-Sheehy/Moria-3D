using UnityEngine;
using System.Collections;

public class TestForMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 1));
        }
	}
}
