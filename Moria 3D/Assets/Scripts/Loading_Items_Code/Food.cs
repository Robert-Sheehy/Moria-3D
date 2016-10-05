using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Food:LootableItem
{
    private int FoodVal;


    public Food(String description, int foodVal ,int level,  int cost)

    {
        Description = description;
        FoodVal = foodVal;
        Level = level;
        Cost = cost;
        ID = 12;

    }
}