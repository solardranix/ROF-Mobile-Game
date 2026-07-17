using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyObjectPoolingScript : MonoBehaviour
{
    public static EnemyObjectPoolingScript currentEn;
    public List<GameObject> poolObjectEn;
    public int pooledAmount = 20;
    public bool willGrow = true;

    List<List<GameObject>> pooledObjectEn;

    void Awake()
    {
        currentEn = this;
    }

    // Use this for initialization
    void Start()
    {
        pooledObjectEn = new List<List<GameObject>>();
        for (int i = 0; i < poolObjectEn.Count; i++)
        {
            pooledObjectEn.Add(new List<GameObject>());
            for (int j = 0; j < pooledAmount; j++)
            {
                GameObject obj = (GameObject)Instantiate(poolObjectEn[i]);
                obj.SetActive(false);
                pooledObjectEn[i].Add(obj);
            }
        }
    }

    public GameObject GetPooledObjectEn(int selectedBullet)
    {
        for (int j = 0; j < pooledObjectEn[selectedBullet].Count; j++)
        {
            if (!pooledObjectEn[selectedBullet][j].activeInHierarchy)
            {
                return pooledObjectEn[selectedBullet][j];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(poolObjectEn[selectedBullet]);
            pooledObjectEn[selectedBullet].Add(obj);

            return obj;
        }

        return null;
    }
}
