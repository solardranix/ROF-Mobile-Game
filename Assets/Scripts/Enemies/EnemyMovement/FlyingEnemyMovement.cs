using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Collider2D))]

public class FlyingEnemyMovement : MonoBehaviour
{
    // what to chase
    private Transform target;

    public float speed = 15.0f;
    public int damage = 10;
    //===============
    Vector3 direction;
    // Use this for initialization
    void Start()
    {
        Direction();
    }

    void OnEnable()
    {
        Direction();
    }

    void Direction()
    {
        target = GameObject.Find("Base").GetComponent<Transform>();
        direction = (target.transform.position - gameObject.transform.position).normalized;
        transform.right = -direction;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
