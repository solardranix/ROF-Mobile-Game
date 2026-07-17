using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minimumLogoTime = 2.0f;

	// Use this for initialization
	private void Start ()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Start wit Blak Screen
        fadeGroup.alpha = 1;

        // Pre load the game
        // Loading Data From Server


        // Get a timestamp of the completion time
        // if loadtime is super fast, give it small buffer time so we can apreciate the logo
        if(Time.time < minimumLogoTime)
        {
            loadTime = minimumLogoTime;
        }
        else
        {
            loadTime = Time.time;
        }
	}
	
	// Update is called once per frame
	private void Update ()
    {
        // Fade-in
        if (Time.time < minimumLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }

        // Fade-out
        if(Time.time > minimumLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minimumLogoTime;
            if(fadeGroup.alpha >= 1)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
	}
}
