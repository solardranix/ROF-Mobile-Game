using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRocket : MonoBehaviour
{
    public static RedRocket redRocket;

    public GameObject rocketArt;
    //Week Bullet moving Forward & You Alwaze Have This
    //stats
    public float bulletMovementSpeed = 40.0f;
    //===============
    Vector3 direction;
    float speed;

    public float destroyTimeForFire = 0.4f;

    void Start()
    {
        redRocket = this;
        Init();
    }

    void OnEnable()
    {
        Init();
        rocketArt.SetActive(true);
    }

    void Init()
    {
        direction = gameObject.transform.right;
        speed = bulletMovementSpeed;
    }
    //============================================================

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            rocketArt.SetActive(false);
            speed = 0;

            Invoke("Destroy", destroyTimeForFire);
        }

        if (col.gameObject.tag == "EnemyBullet")
        {
            col.gameObject.SetActive(false);
            //gameObject.SetActive(false);    //Set Bullet Inactive
            //Shoot();
        }
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
