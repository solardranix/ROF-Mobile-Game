using UnityEngine;
using System.Collections;

public class NormalBulletClass : MonoBehaviour
{
    //Week Bullet moving Forward & You Alwaze Have This
    //stats
    public float bulletMovementSpeed = 25.0f;

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
            gameObject.SetActive(false);    //Set Bullet Inactive
            //Shoot();
        }
    }
}
