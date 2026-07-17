using UnityEngine;
using System.Collections;

public class EnemyLasear : MonoBehaviour 
{

    //stats
    public float bulletMovementSpeed = 10.0f;
    public int damage = 10;


    //===============
    Vector3 direction;

    void Start()
    {
        direction = this.gameObject.transform.right;
    }

    void OnEnable()
    {
        direction = this.gameObject.transform.right;
    }
    //============================================================

    void Update()
    {
        transform.position += direction * bulletMovementSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);    //Set Bullet Inactive
            //TODO: Bullet Collision Sound  & Effect
        }
    }
}
