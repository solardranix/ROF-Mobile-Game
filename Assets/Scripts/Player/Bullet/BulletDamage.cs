using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public static BulletDamage bulletDamage;

    public int damage = 20;
    public int kaboomEffectNumInPool;

    void Start()
    {
        bulletDamage = this;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.DamageEnemy(damage);
            gameObject.SetActive(false);    //Set Bullet Inactive
            //Shoot();
        }
    }
    /*
    public void Shoot()
    {
        GameObject obj = EnemyObjectPoolingScript.currentEn.GetPooledObjectEn(kaboomEffectNumInPool);
        if (obj == null) return;
        //Pool Shoot
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
        //TODO: Bullet Collision Sound & Effect
    }
    */
}
