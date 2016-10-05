using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Armor:LootableItem
{
    internal int AC;
    internal float Weight;
    internal int HitProtection;

    public Armor(String description, int level, float weight, int ArmorValue,  int cost)

    {
        Description = description;
        Level = level;
        Weight = weight;
        AC = ArmorValue;
        Cost = cost;


        ID = 11;
       
    }


}