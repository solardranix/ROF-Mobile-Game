using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class FlyingMachineGun : MonoBehaviour
{
    // what to chase
    private Transform target;
    private Vector3 enemyTarget;

    public float speed = 3.0f;
    public int damage = 10;
    //===============
    Vector3 direction;
    Vector3 newDirection;

    public float radarRadius = 40.0f;
    private float targetDistance;

    public float distanceForShooting = 220.0f;

    //=========== Shooting Value ====================
    public Transform[] enemybulletSpawnerPos;
    public int enemybulletPrefabNumInPool;
    public float fireRate = 2.0f;
    public float rotationSpeed = 0.1f;
    float timeToFire;

    private bool changeMovementMode;


    // Use this for initialization
    void Start()
    {
        timeToFire = 0f;
        Init();
    }

    void OnEnable()
    {
        Init();
    }

    void Init()
    {
        direction = transform.right;
        targetDistance = radarRadius;
        target = null;
    }
    /*
    void Direction()
    {
        target = GameObject.Find("Base").GetComponent<Transform>();
        
        if (this.transform.position.x > 0)
        {
            direction = -this.gameObject.transform.right;
        }
        if (this.transform.position.x < 0)
        {
            direction = this.gameObject.transform.right;
        }
    }
    */
    /*
    void NewDirection()
    {
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
    */
    void Update()
    {
        if (!target)
        {
            BulletFindTarget();
        }

        float Dis;
        Dis = (target.transform.position - this.gameObject.transform.position).sqrMagnitude;

        transform.position += direction * speed * Time.deltaTime;

        if (Dis < distanceForShooting)
        {

            if(speed > 0.4)
            {
                speed -= 0.2f;
            }

            

            RadarRotation();
            direction = transform.right;
            //transform.position += direction * bulletMovementSpeed * Time.deltaTime;

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

    void BulletFindTarget()
    {
        Transform go = GameObject.Find("Base").GetComponent<Transform>();

        float currentTargetDis = 250.0f;

        currentTargetDis = (go.position - transform.position).sqrMagnitude;
        targetDistance = currentTargetDis;
        target = go;

        targetDistance = radarRadius;
    }

    public void RadarRotation()
    {
        float angle;

        if (target)
        {
            angle = FindAngle(transform.right, target.position - transform.position, transform.forward);
            transform.Rotate(Vector3.forward, angle * rotationSpeed * Time.deltaTime);
        }
        else
        {
            angle = 0.0f;
        }
    }

    float FindAngle(Vector3 fromVec, Vector3 toVec, Vector3 upVec)
    {
        if (toVec == Vector3.zero)
        {
            return 0.0f;
        }

        float angle = Vector3.Angle(fromVec, toVec);
        Vector3 normal = Vector3.Cross(fromVec, toVec);

        angle *= Mathf.Sign(Vector3.Dot(normal, upVec));
        angle *= Mathf.Deg2Rad;

        return angle;
    }
}
