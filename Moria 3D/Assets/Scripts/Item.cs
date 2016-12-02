using UnityEngine;
using System.Collections;

public class Item { 
    
    public enum list : int
    {
        emptySpace = 0,
        wall = 1,
        door = 2,
        hiddenDoor = 3,
        //placeholders for mesh replacement
        cube = 4,
        sphere = 5,
        cylinder = 6,
        Weapon = 101,
        Armor = 102,
        Food = 103,

    }
}
