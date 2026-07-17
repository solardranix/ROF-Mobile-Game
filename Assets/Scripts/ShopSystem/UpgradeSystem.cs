using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem upgradeSystem;

    [SerializeField]
    private int coin;
    [SerializeField]
    private int bestScore;
    [SerializeField]
    private string userName;

    public int currentWeaponID = 1;

    public Text CoinCountText;
    public Text bestScoreText;
    public Text bestScoreShareText;
    //userName Menu
    public Text inputUserName;
    public GameObject inputFieldDeactivation;
    public Text[] changeText;

    public Text userNameHUD;
    public GameObject userNameBTN;


    private SaveLoad saveLoad;

    // Use this for initialization
    void Start()
    {
        upgradeSystem = this;
        saveLoad = GameObject.FindObjectOfType<SaveLoad>();
        // First game loading
        saveLoad.Loading();
        UpdateUI();
        UpdateScoreUI();
        UpdateUserUI();
        if (userName == "Guest")
        {
            userNameBTN.SetActive(true);
        }
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        UpdateUI();
    }
    
    public void RediuceCoin(int amount)
    {
        coin -= amount;
        UpdateUI();
    }

    public bool RequestCoin(int amount)
    {
        if(amount <= coin)
        {
            return true;
        }
        return false;
    }

    public int GetCoinInfo()
    {
        return coin;
    }

    public void SetCoinInfo(int amount)
    {
        coin = amount;
        UpdateUI();
    }

    public int GetScoreInfo()
    {
        return bestScore;
    }

    public void SetScoreInfo(int amount)
    {
        bestScore = amount;
        UpdateScoreUI();
    }

    public string GetUserNameInfo()
    {
        return userName;
    }
    
    //Set UserName By Player
    public void ChangeUserNameInfo()
    {
        if(userName == "Guest")
        {
            if(inputUserName.text != "")
            {
                userName = inputUserName.text;
                changeText[0].text = Fa.faConvert("نام کاربری شما:");
                changeText[1].text = userName;
                changeText[1].color = Color.green;
                inputFieldDeactivation.SetActive(false);
                SetUserNameInfo(userName);

                SaveLoad.saveLoad.Saving();
            }
            else
            {
                changeText[0].color = Color.red;
            }
        }
    }

    public void SetUserNameInfo(string userNameData)
    {
        userName = userNameData;
        UpdateUserUI();
    }

    void UpdateUserUI()
    {
        userNameHUD.text = userName;
    }

    void UpdateUI()
    {
        CoinCountText.text = coin.ToString();
    }

    void UpdateScoreUI()
    {
        bestScoreText.text = bestScore.ToString() +" " + Fa.faConvert("بهترین رکورد: ");
        bestScoreShareText.text = bestScore.ToString() + " " + Fa.faConvert("بهترین رکورد: ");
        ShareScore.myBestScore = bestScore;
    }
}

