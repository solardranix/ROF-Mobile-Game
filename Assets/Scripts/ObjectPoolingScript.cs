using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolingScript : MonoBehaviour
{
    public static ObjectPoolingScript objectPoolingScript;
    public List<GameObject> poolObject;
    public int pooledAmount = 20;
    public bool willGrow = true;

    List<List<GameObject>> pooledObject;

    void Awake()
    {
        objectPoolingScript = this;
    }

	// Use this for initialization
	public void InstantiateObjectPool()
    {
        pooledObject = new List<List<GameObject>>();
        for(int i = 0; i < poolObject.Count; i++)
        {
            pooledObject.Add(new List<GameObject>());
            for (int j = 0; j < pooledAmount; j++)
            {
                GameObject obj = (GameObject)Instantiate(poolObject[i]);
                obj.SetActive(false);
                pooledObject[i].Add(obj);
                //Debug.Log("Bullet Pooled");
            }
        }
	}
	
	public GameObject GetPooledObject(int selectedBullet)
    {
        for(int j = 0; j < pooledObject[selectedBullet].Count; j++)
        {
            if(!pooledObject[selectedBullet][j].activeInHierarchy)
            {
                return pooledObject[selectedBullet][j];
            }
        }

        if(willGrow)
        {
            GameObject obj = (GameObject)Instantiate(poolObject[selectedBullet]);
            pooledObject[selectedBullet].Add(obj);
            return obj;
        }

        return null;
    }
}
