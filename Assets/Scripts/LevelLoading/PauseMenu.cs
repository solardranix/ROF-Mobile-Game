using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu pauseMenu;

    public GameObject pauseMenuOBJ;

    public string sceneName;

    public Text newScoreTXT;
    public Text BestScoreTXT;


    void Start()
    {
        pauseMenu = this;
    }

    public void OnPauseClick()
    {
        Time.timeScale = 0;

        newScoreTXT.text = GameManager.gameManager.score.ToString() + " " + Fa.faConvert("رکورد جدید: ");
        BestScoreTXT.text = GameManager.gameManager.bestScore.ToString() + " " + Fa.faConvert("بهترین رکورد: ");


        pauseMenuOBJ.SetActive(true);
    }

	public void OnResumeClick()
    {
        pauseMenuOBJ.SetActive(false);
        if(Time.timeScale <1)
        {
            Time.timeScale = 1;
        }
    }

    public void OnMainMenuClick()
    {

        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }

        if (GameManager.gameManager.score > GameManager.gameManager.bestScore)
        {
            SaveLoad.saveLoad.LevelSaving(GameManager.gameManager.score);
        }
        else
        {
            SaveLoad.saveLoad.LevelSaving(GameManager.gameManager.bestScore);
        }

        SceneManager.LoadScene(sceneName);
    }
}
