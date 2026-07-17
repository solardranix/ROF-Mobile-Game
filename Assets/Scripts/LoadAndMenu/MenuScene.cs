using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapsellSDK;
using System;

public class MenuScene : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.7f;

    public RectTransform menuContainer;
    public GameObject userNameMenu;

    private Vector3 desiredMenuPosition;

    // Use this for initialization
    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Start wit Blak Screen
        fadeGroup.alpha = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Fade-in
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

        // Menu Navication (smoooth)
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
    }


    private void NavigateTo(int menuIndex)
    {
        switch(menuIndex)
        {
            // 0 && default case = Main Menu
            default:
            case 0:
                desiredMenuPosition = Vector3.zero;
                break;
            // 1 = Coin Shop
            case 1:
                desiredMenuPosition = Vector3.up * 800;
                break;
            // 2 = Weapon Menu
            case 2:
                desiredMenuPosition = Vector3.right * 1280;
                break;
            // 3 = LeaderBoard
            case 3:
                desiredMenuPosition = Vector3.up * 1600;
                break;
            // 4 = Level Selection
            case 4:
                desiredMenuPosition = Vector3.down * 800;
                break;
        }
    }

    public void OnPlayTouch()
    {
        // Save After Buy Weapon
        SaveLoad.saveLoad.Saving();
        SceneManager.LoadScene("Endless");
    }

    public void OnHomeTouch()
    {
        NavigateTo(0);
    }

    public void OnShopTouch()
    {
        if (desiredMenuPosition.x > 0f)
        {
            StartCoroutine(GoToWeaponUpgrade(1));
        }
        else
        {
            NavigateTo(1);
        }
    }

    public void OnUpgradeTouch()
    {
        if(!(desiredMenuPosition.x> 0f))
        {
            StartCoroutine(GoToWeaponUpgrade(2));
        }
    }

    IEnumerator GoToWeaponUpgrade(int targetMenu) 
    {
        NavigateTo(0);

        yield return new WaitForSeconds(0.7f);

        if (desiredMenuPosition == Vector3.zero)
        {
            NavigateTo(targetMenu);
        }
        yield break;
    }
    

    public void OnLeaderBoardTouch()
    {
        if(desiredMenuPosition.x> 0f)
        {
            StartCoroutine(GoToWeaponUpgrade(3));
        }
        else
        {
            NavigateTo(3);
        }   
        HSController.Instance.startGetScores();
    }

    public void OnAchievementsTouch()
    {
        if (desiredMenuPosition.x > 0f)
        {
            StartCoroutine(GoToWeaponUpgrade(4));
        }
        else
        {
            NavigateTo(4);
        }
        HSController.Instance.startGetScores();
    }

    public void OnUserNameChange()
    {
        userNameMenu.SetActive(true);
    }

    public void OnCreditTouch()
    {
        Debug.Log("Credit BTN");
    }
}
