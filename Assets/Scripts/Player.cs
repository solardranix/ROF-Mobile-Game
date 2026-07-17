using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public static Player player;

    public int health = 200;
    public int fullHealth;
    public int enemyColDamage = 50;

    void Start()
    {
        player = this;
        player.fullHealth = player.health;
    }
    
    public void DamagePlayer(int damage)
    {
        player.health -= damage;
        float damageFl = (float)damage / player.fullHealth;
        GameManager.AddDamageToPlayer(damageFl);

        if(player.health <=0)
        {
            GameManager.KillPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyBullet")
        {
            int colDamage = col.gameObject.GetComponent<EnemyLasear>().damage;
            DamagePlayer(colDamage);
            col.gameObject.SetActive(false);    //Set EnemyBullet Inactive
        }

        if (col.gameObject.name.StartsWith("EnemyTank"))
        {
                DamagePlayer(player.enemyColDamage);
                col.gameObject.SetActive(false);    //Set EnemyBullet Inactive
        }
    }
}
