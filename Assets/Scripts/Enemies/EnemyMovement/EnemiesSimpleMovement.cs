using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class EnemiesSimpleMovement : MonoBehaviour
{
    // what to chase
    private Transform target;
    private Vector3 enemyTarget;

    public float speed = 3.0f;
    public int damage = 10;
    //===============
    Vector3 direction;

    public float distanceForShooting = 220.0f;

    //=========== Shooting Value ====================
    public Transform[] enemybulletSpawnerPos;
    public int enemybulletPrefabNumInPool;
    public float fireRate = 2.0f;
    float timeToFire;


    // Use this for initialization
    void Start()
    {
        timeToFire = 0f;
        Direction();
    }

    void OnEnable()
    {
        Direction();
    }

    void Direction()
    {
        target = GameObject.Find("Base").GetComponent<Transform>();
        if (this.transform.position.x > 0)
        {
            enemyTarget = new Vector3((target.position.x + 3.0f), target.position.y, target.position.z);
        }
        if (this.transform.position.x < 0)
        {
            enemyTarget = new Vector3((target.position.x - 3.0f), target.position.y, target.position.z);
        }
        
        direction = (enemyTarget - gameObject.transform.position).normalized;
        transform.right = -direction;
    }

    void Update()
    {
        float Dis;
        Dis = (target.transform.position - gameObject.transform.position).sqrMagnitude;

        transform.position += direction * speed * Time.deltaTime;

        if(Dis < distanceForShooting)
        {
            if (Time.time > timeToFire)
            {
                timeToFire = Time.time + fireRate;
                Shoot();
            }
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
    }
}
