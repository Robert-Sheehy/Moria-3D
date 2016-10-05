using UnityEngine;
using System.Collections;

/*

    usage:
            void Update(){
                KeyController.runOnKey(KeyCode.W, KeyController.keyPressType.keyDown, moveForwards());
            }

            void moveForwards(){
                //move forwards            
            }

*/
public class KeyController
{
    public delegate void keyPressEvent();

    public enum keyPressType
    {
        keyDown,
        keyUp,
        keyHold
    }

    public static void runOnKey(KeyCode key, keyPressType type, keyPressEvent action)
    {
        switch (type)
        {
            case keyPressType.keyDown:
                if (Input.GetKeyDown(key)) action();
                break;
            case keyPressType.keyHold:
                if (Input.GetKey(key)) action();
                break;
            case keyPressType.keyUp:
                if (Input.GetKeyUp(key)) action();
                break;
        }
    }
}
