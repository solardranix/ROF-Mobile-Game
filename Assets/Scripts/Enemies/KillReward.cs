using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillReward : MonoBehaviour
{
    public float speed = 3.0f;
    private Vector3 target;
    //===============
    Vector3 direction;
    // Use this for initialization
    void Start ()
    {
        target = new Vector3(-17.0f, 10.0f, 0f);
        direction = (target - gameObject.transform.position).normalized;
        transform.right = -direction;

    }

    void OnEnable()
    {
        GameManager.CoinReward(10);
        direction = (target - gameObject.transform.position).normalized;
        transform.right = -direction;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if(transform.position == target)
        {
            gameObject.SetActive(false);
        }
    }
}
