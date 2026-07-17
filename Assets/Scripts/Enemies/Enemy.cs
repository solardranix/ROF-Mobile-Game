using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    [System.Serializable]
    public class EnemyStats
    {

        public int health = 100;
        public int fullHealth;

        public int explodingNumInPool = 4;

        public bool killReward;
        public int killRewardNumInPool;
    }
    
    public EnemyStats enemyStats = new EnemyStats();
    public bool flyingEnemy = true;
    void Start()
    {
        enemyStats.fullHealth = enemyStats.health;
        enemyStats.killReward = false;
    }

    public void DamageEnemy(int damage)
    {
        enemyStats.health -= damage;

        if (enemyStats.health <= 0)
        {
            GameManager.KillEnemy(this);
            Shoot(enemyStats.explodingNumInPool);

            if (enemyStats.killReward)
            {
                RewardInit(enemyStats.killRewardNumInPool);

                enemyStats.killReward = false;
            }    
        }
    }

    public void RewardInit(int numInPool)
    {
        GameObject obj = EnemyObjectPoolingScript.currentEn.GetPooledObjectEn(numInPool);
        if (obj == null) return;
        //Pool Shoot
        obj.transform.position = transform.position;
        //obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
        //Play Shooting Sound
    }
    public void Shoot(int numInPool)
    {
        GameObject obj = EnemyObjectPoolingScript.currentEn.GetPooledObjectEn(numInPool);
        if (obj == null) return;
        //Pool Shoot
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
        //Play Shooting Sound
    }
}
