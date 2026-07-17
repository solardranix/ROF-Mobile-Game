using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolderInGame : MonoBehaviour
{
    public static WeaponHolderInGame weaponHolderInGame;
    //=========== Shooting Value ====================
    public Transform[] bulletSpawnerPos;
    public int spawnerCount;

    public float fireRate = 0.3f;
    
    float timeToFire;

    // Use this for initialization
    void Start()
    {
        weaponHolderInGame = this;
        timeToFire = 0f;
    }

    public void Attack()
    {
        if (Time.time > timeToFire)
        {
            switch (spawnerCount)
            {
                case 1:
                    {
                        GameObject obj = ObjectPoolingScript.objectPoolingScript.GetPooledObject(bulletNumberInPool);
                        if (obj == null) return;
                        //Pool Shoot
                        obj.transform.position = bulletSpawnerPos[0].position;
                        obj.transform.rotation = bulletSpawnerPos[0].rotation;
                        obj.SetActive(true);
                    }
                    break;
                case 2:
                    {
                        for (int i = 1; i <= 2; i++)
                        {
                            GameObject obj = ObjectPoolingScript.objectPoolingScript.GetPooledObject(bulletNumberInPool);
                            if (obj == null) return;
                            //Pool Shoot
                            obj.transform.position = bulletSpawnerPos[i].position;
                            obj.transform.rotation = bulletSpawnerPos[i].rotation;
                            obj.SetActive(true);
                        }
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i <= 2; i++)
                        {
                            GameObject obj = ObjectPoolingScript.objectPoolingScript.GetPooledObject(bulletNumberInPool);
                            if (obj == null) return;
                            //Pool Shoot
                            obj.transform.position = bulletSpawnerPos[i].position;
                            obj.transform.rotation = bulletSpawnerPos[i].rotation;
                            obj.SetActive(true);
                        }
                    }
                    break;
            }




            timeToFire = Time.time + fireRate;
        }


         
    }



    //-----------------weaponHolder value---------------
    public Image itemImage;
    public int bulletNumberInPool;
}
