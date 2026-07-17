using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour 
{
    //=========== Shooting Value ====================
    public Transform[] enemybulletSpawnerPos;
    public int enemybulletPrefabNumInPool;
    public float fireRate = 2.0f;
    float timeToFire;

	// Use this for initialization
	void Start () 
    {
        timeToFire = 0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.time > timeToFire)
        {
            timeToFire = Time.time + fireRate;
            Shoot();
        }
	}

    public void Shoot()
    {
        for (int i = 0; i < enemybulletSpawnerPos.Length; i++)
        {
            GameObject obj = EnemyObjectPoolingScript.currentEn.GetPooledObjectEn(enemybulletPrefabNumInPool);
            if (obj == null) return;
            //Pool Shoot
            obj.transform.position = enemybulletSpawnerPos[i].position;
            obj.transform.rotation = enemybulletSpawnerPos[i].rotation;
            obj.SetActive(true);
        }
        //Play Shooting Sound
    }
}
