using UnityEngine;
using System.Collections;

public class BulletDestroyCamCol : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            col.gameObject.SetActive(false);
        }
    }
}
