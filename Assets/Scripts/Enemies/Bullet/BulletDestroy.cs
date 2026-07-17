using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour 
{
    public float destroyTime = 1.0f;
    public int kaboomEffectNumInPool;
    void OnEnable()
    {
        Invoke("Destroy", destroyTime);
    }

    void Destroy()
    {
        Shoot();
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    public void Shoot()
    {
        GameObject obj = EnemyObjectPoolingScript.currentEn.GetPooledObjectEn(kaboomEffectNumInPool);
        if (obj == null) return;
        //Pool Shoot
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
        //Play Shooting Sound
    }
}
