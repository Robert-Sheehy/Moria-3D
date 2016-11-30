using UnityEngine;
using System.Collections;

public class Item { 
    
    public enum list : int
    {
        emptySpace = 0,
        wall = 1,
        door = 2,
        hiddenDoor = 3,
        weapon = 10,
        armor = 11,
        food = 12
    }
}
