
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;

    public float startTime;
    public GameObject gameOver;

    public string userName;
    public int score;
    public int bestScore;
    public int coin;
    private int comboCounter;
    public GameObject coinReward;
    public float comboRate = 5.0f;
    float timeToCombo;

    public int restoreHealthPrice;
    //-------------------------- Singelton Pattern --------------------------
    public static GameManager gameManager;

    // Use this for initialization
    void Awake()
    {
        uiManager = UIManager.Instance;
        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
    }

    void Start()
    {
        startTime = Time.time;
        timeToCombo = 0.0f;
        score = 0;
        comboCounter = 0;
        SaveLoad.saveLoad.LevelWeaponLoading();
        gameManager.uiManager.UpdateCoin(coin);
        restoreHealthPrice = 500;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void AddDamageToPlayer(float damage)
    {
        gameManager.uiManager.UpdateHealth(damage);
    }

    //Kill Player
    public static void KillPlayer()
    {
        Time.timeScale = 0;
        gameManager.uiManager.ScorePanelInit(gameManager.score, gameManager.bestScore, gameManager.restoreHealthPrice);
        gameManager.gameOver.SetActive(true);
    }

    //Enemy
    public static void KillEnemy(Enemy enemy)
    {
        //TODO: play Daed Sound Or Animation
        enemy.enemyStats.health = enemy.enemyStats.fullHealth;
        enemy.gameObject.SetActive(false);

        // Update Score UI
        gameManager.score += enemy.enemyStats.fullHealth;
        gameManager.uiManager.UpdateScore(gameManager.score);
    }

    public static void CoinReward(int coin)
    {
        gameManager.coin += coin;
        gameManager.uiManager.UpdateCoin(gameManager.coin);
    }
}
