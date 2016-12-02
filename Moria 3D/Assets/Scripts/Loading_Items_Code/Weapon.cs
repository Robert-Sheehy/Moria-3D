using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Weapon:LootableItem
{
    internal int Hit;
    internal int Damage;
    internal float Weight;
    internal int HitBuff, DamBuff;

    public Weapon(String description, int hit, int dam, float weight,int level, int cost)

    {
        Description = description;
        Hit = hit;
        Damage = dam;
        Weight = weight;
        Cost = cost;
        ID = (int)Item.list.weapon;
    }

    public Weapon(String description, int hit, int dam, float weight,int level, int cost,int buffHit, int buffDam):this(description,  hit,  dam, weight,level,  cost)

    {
        HitBuff = buffHit;
        DamBuff = buffDam;
        

    }


}