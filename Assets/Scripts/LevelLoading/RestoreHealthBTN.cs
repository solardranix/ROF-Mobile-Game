using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class RestoreHealthBTN : MonoBehaviour
{
    private UIManager uimanager;
    public string sceneName;

    public float CoolDown = 0.1f;

    public Image RestoreBTNImages;

    // Use this for initialization
    void Start ()
    {
        uimanager = UIManager.Instance;

    }
	void OnEnable()
    {
        RestoreBTNImages.fillAmount = 1.0f;
    }

	// Update is called once per frame
	void Update ()
    {
            //energyBarButton.interactable = false;

        if (RestoreBTNImages.fillAmount > 0.0f)
        {
            RestoreBTNImages.fillAmount -= ((float)CoolDown / 60);
        }
        else if (RestoreBTNImages.fillAmount <= 0.0f)
        {
            //GameAnalytics.NewDesignEvent("Score", GameManager.gameManager.score);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Endless", "Endless Center Base", "Score", GameManager.gameManager.score); // with score

            if (GameManager.gameManager.score > GameManager.gameManager.bestScore)
            {
                SaveLoad.saveLoad.LevelSaving(GameManager.gameManager.score);
            }
            else
            {
                SaveLoad.saveLoad.LevelSaving(GameManager.gameManager.bestScore);
            }
            //Set Ative Menu = False
            if (Time.timeScale < 1)
            {
                Time.timeScale = 1;
            }

            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnRestoreClick()
    {
        Player.player.health = Player.player.fullHealth;
        uimanager.RestoreHealth();


        GameManager.gameManager.gameOver.SetActive(false);

        //Decrese Coin By Restor Price
        GameManager.gameManager.coin -= GameManager.gameManager.restoreHealthPrice;
        //Updade coin ui
        uimanager.UpdateCoin(GameManager.gameManager.coin);

        if (GameManager.gameManager.score > GameManager.gameManager.bestScore)
        {
            SaveLoad.saveLoad.LevelSaving(GameManager.gameManager.score);
        }
        else
        {
            SaveLoad.saveLoad.LevelSaving(GameManager.gameManager.bestScore);
        }

        GameManager.gameManager.restoreHealthPrice *= 2;

        

        //Destroy All Enemy In Scenes with out givinng Score
        //for(int i = 0; i < ObjectPoolingScript.)

        Time.timeScale = 1;
    }
}
