using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInit : MonoBehaviour
{
    //public static WeaponInit weaponInit;

    //---------------------  Weapon Load Value -------------
    public List<GameObject> weaponHolderList = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        //weaponInit = this;
    }
    
    public void InitLevelWeapon(int bulletNumInPool, int bulletDamage, float fireRate, int spawnerCount, string weapoinSpirteName)
    {
        for (int i = 0; i < weaponHolderList.Count; i++)
        {

            WeaponHolderInGame weaponHolderInGame = weaponHolderList[i].GetComponent<WeaponHolderInGame>();

            weaponHolderInGame.bulletNumberInPool = bulletNumInPool;
            
            weaponHolderInGame.fireRate = fireRate;
            weaponHolderInGame.spawnerCount = spawnerCount;
            weaponHolderInGame.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + weapoinSpirteName);
        }

        //Set Current Weapon Bullet Damage
        BulletDamage currentBullet = ObjectPoolingScript.objectPoolingScript.poolObject[bulletNumInPool].GetComponent<BulletDamage>();
        currentBullet.damage = bulletDamage;


        // Instansiate Bullet Pool After Set Damage
        ObjectPoolingScript.objectPoolingScript.InstantiateObjectPool();
    }
    
    public void InitScoreAndCoin(string userName, int bestScore, int coin)
    {
        GameManager.gameManager.userName = userName;
        GameManager.gameManager.bestScore = bestScore;
        GameManager.gameManager.coin = coin;
    }

}
