using UnityEngine;
using System.Collections;
using System;

public class CharacterControl : MonoBehaviour {

    enum CharacterState { WaitingForInput,Moving}
    LevelController level;
    CharacterState currently = CharacterState.WaitingForInput;
    float cameraDistance = 2;
    float cameraHeight = 1;
    Vector3 currentTarget;
    int currenti, currentj;
    Vector3 lerpStart;
    Vector3 lerpFinish;

    private float timer;

    float duration = .5f;
    private float FocusDistance =5f;
    private bool isFirstTime;


    // Use this for initialization
    void Start() {
        currenti = 2;
        currentj = 5;
        transform.position = worldPositionOf(currenti, currentj);

        level = FindObjectOfType<LevelController>();
	}
	
	// Update is called once per frame
	void Update () {

        updateCamera();
   
        switch (currently)
        {

            case CharacterState.WaitingForInput:
            
                isFirstTime = true;
        if (shouldMoveForward()) MoveForward();
        if (shouldMoveForwardRight()) MoveForwardRight();
		if (shouldMoveForwardLeft()) MoveForwardLeft();
		if (shouldMoveLeft()) MoveLeft();
		if (shouldMoveRight()) MoveRight();
		if(shouldMoveBackRight()) MoveBackRight();
		if(shouldMoveBackLeft()) MoveBackLeft();
		if(shouldMoveBackwards()) MoveBackwards();

                break;

            case CharacterState.Moving:

         
                if ((level.queryLevel((int)lerpFinish.x, (int)lerpFinish.z) == 1))
                {
             
                    currently = CharacterState.WaitingForInput;
                    currenti = (int)lerpStart.x;
                    currentj = (int)lerpStart.z;
                    transform.position = lerpStart;
             
                }
             
                else
                {
                    print("actually");
                    timer += Time.deltaTime;
                    transform.position = Vector3.Lerp(lerpStart, lerpFinish, timer / duration);
                    transform.rotation = Quaternion.LookRotation(lerpFinish - lerpStart);


                    if (timer > duration)
                    {
                        transform.position = lerpFinish;
                        currently = CharacterState.WaitingForInput;
                    }
                }
   
                break;


        }


	}

    private void updateCamera()
    {
        Camera.main.transform.position = transform.position - cameraDistance * transform.forward + cameraHeight * transform.up;
        Camera.main.transform.LookAt(transform.position+ FocusDistance *transform.forward);
        

        
    }

    private Vector3 worldPositionOf(int currenti, int currentj)
    {
        return new Vector3(currenti, 0, currentj);
    }


   

    private void MoveForward()
    {
        print("Forward" + transform.forward);

        lerpStart = transform.position;
        Vector3 destDir = Quaternion.AngleAxis(0, Vector3.up) * transform.forward;

        print("F i before" + currenti + " j before" + currentj);

	    if (destDir.x > 0.1f) currenti++;
        else if (destDir.x < -0.1f) currenti--;
        if (destDir.z > 0.1f) currentj++;
        else if (destDir.z < -0.1f) currentj--;

        lerpFinish = worldPositionOf(currenti, currentj);

        print("F i after" + currenti + " j after " + currentj);

        timer = 0;
        currently = CharacterState.Moving;
    }

    private void MoveForwardRight()
    {
        print("fr");
        print(transform.forward);
        lerpStart = transform.position;
        Vector3 destDir =  Quaternion.AngleAxis(45, Vector3.up) * transform.forward;
        print(destDir.x + " " + destDir.y + " " + destDir.z);
        print("FR i before" + currenti + " j before" + currentj);

        if (destDir.x > 0.1f) currenti++;
        else if (destDir.x < -0.1f) currenti--;
        if (destDir.z > 0.1f) currentj++;
        else if (destDir.z < -0.1f) currentj--;


        lerpFinish = worldPositionOf(currenti, currentj);

        print("FR i after" + currenti + " j after " + currentj);

        timer = 0;
        currently = CharacterState.Moving;
    }

    private void MoveForwardLeft()
    {
        print("fL");

        lerpStart = transform.position;
        Vector3 destDir = Quaternion.AngleAxis(-45, Vector3.up) * transform.forward;

        print("FL i before" + currenti + " j before" + currentj);

        if (destDir.x > 0.1f) currenti++;
        else if (destDir.x < -0.1f) currenti--;
        if (destDir.z > 0.1f) currentj++;
        else if (destDir.z < -0.1f) currentj--;

        lerpFinish = worldPositionOf(currenti, currentj);

        print("FL i after" + currenti + " j after " + currentj);

        timer = 0;
        currently = CharacterState.Moving;
    }

    private void MoveLeft()
    {
        print("l");

        lerpStart = transform.position;
        Vector3 destDir = Quaternion.AngleAxis(-90, Vector3.up) * transform.forward;

        print("L i before" + currenti + " j before" + currentj);

        if (destDir.x > 0.1f) currenti++;
        else if (destDir.x < -0.1f) currenti--;
        if (destDir.z > 0.1f) currentj++;
        else if (destDir.z < -0.1f) currentj--;

        lerpFinish = worldPositionOf(currenti, currentj);

        print("L i after" + currenti + " j after " + currentj);

        timer = 0;
        currently = CharacterState.Moving;
    }

    private void MoveRight()
    {
        print(transform.forward);
        print("R");

        lerpStart = transform.position;
        Vector3 destDir = Quaternion.AngleAxis(90, Vector3.up) * transform.forward;

        print(destDir);
        print("R i before" + currenti + " j before" + currentj);

        if (destDir.x > 0.1f) currenti++;
        else if (destDir.x < -0.1f) currenti--;
        if (destDir.z> 0.1f) currentj++;
        else if (destDir.z < -0.1f) currentj--;

        lerpFinish = worldPositionOf(currenti, currentj);

        print("R i after" + currenti + " j after " + currentj);

        timer = 0;
        currently = CharacterState.Moving;
    }


    private void MoveBackLeft()
    {
        print("BL");

        lerpStart = transform.position;
        Vector3 destDir = Quaternion.AngleAxis(-135, Vector3.up) * transform.forward;

        print("BL i before" + currenti + " j before" + currentj);

        if (destDir.x > 0.1f) currenti++;
        else if (destDir.x < 0.1f) currenti--;
        if (destDir.z > 0.1f) currentj++;
        else if (destDir.z < -0.1f) currentj--;

        lerpFinish = worldPositionOf(currenti, currentj);

        print("BL i after" + currenti + " j after " + currentj);

        timer = 0;
        currently = CharacterState.Moving;
    }

        private void MoveBackRight()
    {
        print("BR");

        lerpStart = transform.position;
        Vector3 destDir = Quaternion.AngleAxis(135, Vector3.up) * transform.forward;

        print("BR i before" + currenti + " j before" + currentj);

        if (destDir.x > 0.1f) currenti++;
        else if (destDir.x < -0.1f) currenti--;
        if (destDir.z > 0.1f) currentj++;
        else if (destDir.z < -0.1f) currentj--;

        lerpFinish = worldPositionOf(currenti, currentj);

        print("BR i after" + currenti + " j after " + currentj);

        timer = 0;
        currently = CharacterState.Moving;
    }

    private void MoveBackwards()
    {
        print("B");

        lerpStart = transform.position;
        Vector3 destDir = Quaternion.AngleAxis(180, Vector3.up) * transform.forward;

        print("B i before" + currenti + " j before" + currentj);

        if (destDir.x > 0.1f) currenti++;
        else if (destDir.x < -0.1f) currenti--;
        if (destDir.z > 0.1f) currentj++;
        else if (destDir.z < -0.1f) currentj--;

        lerpFinish = worldPositionOf(currenti, currentj);

        print("B i after" + currenti + " j after " + currentj);

        timer = 0;
        currently = CharacterState.Moving;
    }


    private bool shouldMoveForwardLeft()
    {
        return Input.GetKey(KeyCode.Q);
    }

    private bool shouldMoveForward()
    {
        return Input.GetKey(KeyCode.W);
    }
    private bool shouldMoveForwardRight()
    {
        return Input.GetKey(KeyCode.E);
    }

    private bool shouldMoveBackRight()
    {
        return Input.GetKey(KeyCode.C);
    }

    private bool shouldMoveBackwards()
    {
        return Input.GetKey(KeyCode.X);
    }

    private bool shouldMoveBackLeft()
    {
        return Input.GetKey(KeyCode.Z);
    }


    private bool shouldMoveRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    private bool shouldMoveLeft()
    {
        return Input.GetKey(KeyCode.A);
    }




}

