using UnityEngine;
using System.Collections;

public class PlayerShootingToch : MonoBehaviour
{
    //=========== Shooting Value ====================
    public Transform bulletSpawnerPos;
    public int bulletPrefabNumInPool;
    public float fireRate = 0.3f;
    float timeToFire;

    // Use this for initialization
    void Start()
    {
        timeToFire = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void touchAttack()
    {
        if (Time.time > timeToFire)
        {
            timeToFire = Time.time + fireRate;
            Attack();
        }
    }

    public void Attack()
    {
        GameObject obj = ObjectPoolingScript.objectPoolingScript.GetPooledObject(bulletPrefabNumInPool);
        if (obj == null) return;
        //Pool Shoot
        obj.transform.position = bulletSpawnerPos.position;
        obj.transform.rotation = bulletSpawnerPos.rotation;
        obj.SetActive(true);
        //Play Shooting Sound
    }
}
