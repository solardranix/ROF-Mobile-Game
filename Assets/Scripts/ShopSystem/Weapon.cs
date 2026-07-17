using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public int weaponID;

    //Only Enter The Bought Weapom Sprite name
    public string spriteName;

    public int weaponPrice;
    public bool bought;
    public int upgradeLevel;
    //Stats
    public float fireRate;
    public int spawnerCount;
    public int damage;
    //public int 
    public int bulletNumInPool;
}
