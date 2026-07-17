using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    public void OnLoadScene()
    {
        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
         
        SceneManager.LoadScene(sceneName);
    }
}
