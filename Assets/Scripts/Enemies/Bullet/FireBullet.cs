using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    //Week Bullet moving Forward & You Alwaze Have This
    //stats
    public float bulletMovementSpeed = 10f;

    //===============
    Vector3 direction;

    void Start()
    {
        direction = gameObject.transform.right;
    }

    void OnEnable()
    {
        direction = gameObject.transform.right;
    }
    //============================================================

    void Update()
    {
        transform.position += direction * bulletMovementSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyBullet")
        {
            col.gameObject.SetActive(false);

            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.1f, gameObject.transform.localScale.y + 0.1f, gameObject.transform.localScale.z);
            //gameObject.SetActive(false);    //Set Bullet Inactive
            //Shoot();
        }
    }

}
