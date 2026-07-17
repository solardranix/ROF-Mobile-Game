using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using TapsellSDK;
using TapsellSimpleJSON;
using ArabicSupport;
using GameAnalyticsSDK;

public class TapsellAdVideoPlay : MonoBehaviour
{
    public static bool available = false;
    public static TapsellAd ad = null;
    public static TapsellNativeBannerAd nativeAd = null;

    public GameObject rewardMenu;

    //===============
    public Text errorTXT;
    public GameObject ErrorPanel;

    public const string rewardAdId = "5a342adc799e6f000146f937";
    //public const string nativeBannerID = "5a391be77a126d00017ff8ad";

    void Start()
    {
        // Use your tapsell key for initialization
        Tapsell.initialize("lkrsegldfqjgfeoiqcktlekjatmcnriramoqegtndjhihjksaqichrmbjkmnirgtttkgkf");

        Debug.Log("Tapsell Version: " + Tapsell.getVersion());
        Tapsell.setDebugMode(true);
        Tapsell.setPermissionHandlerConfig(Tapsell.PERMISSION_HANDLER_AUTO);
        Tapsell.setRewardListener(
            (TapsellAdFinishedResult result) =>
            {
                // onFinished, you may give rewards to user if result.completed and result.rewarded are both True
                Debug.Log("onFinished, adId:" + result.adId + ", zoneId:" + result.zoneId + ", completed:" + result.completed + ", rewarded:" + result.rewarded);

                // You can validate suggestion from you server by sending a request from your game server to tapsell, passing adId to validate it
                if (result.completed && result.rewarded)
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 40);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();

                    rewardMenu.SetActive(true);

                    GameAnalytics.NewDesignEvent("Ad Reward Completed");
                    GameAnalytics.NewBusinessEvent("USD", 1, "Coin Pack", "Video Ad", "Coin Shop");
                    // empty ad adrees
                    TapsellAdVideoPlay.available = false;
                    TapsellAdVideoPlay.ad = null;

                    validateSuggestion(result.adId);
                }
            }
        );

        //Tapsell.requestBannerAd(nativeBannerID, BannerType.BANNER_320x50, Gravity.BOTTOM, Gravity.CENTER);
    }

    public void validateSuggestion(string suggestionId)
    {
        try
        {
            string ourPostData = "{\"suggestionId\":\"" + suggestionId + "\"}";
            System.Collections.Generic.Dictionary<string, string> headers = new System.Collections.Generic.Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");

            byte[] pData = System.Text.Encoding.ASCII.GetBytes(ourPostData.ToCharArray());

            WWW api = new WWW("http://api.tapsell.ir/v2/suggestions/validate-suggestion", pData, headers);
            StartCoroutine(WaitForRequest(api));
        }
        catch (UnityException ex)
        {
            Debug.Log(ex.Message);
        }
        return;
    }

    IEnumerator WaitForRequest(WWW data)
    {
        Debug.Log("my start waiting...");
        yield return data; // Wait until the download is done
        if (data.error != null)
        {
            Debug.Log("my server error is " + data.error);
        }
        else
        {
            Debug.Log("my server result is " + data.text);

            JSONNode node = JSON.Parse(data.text);
            bool valid = node["valid"].AsBool;
            if (valid)
            {
                
                // if suggestion is valid, you can give in game gifts to the user
                Debug.Log("Ad is valid");

                errorTXT.text = Fa.faConvert("Ad is valid");
                ErrorPanel.SetActive(true);
            }
            else {
                Debug.Log("Ad is not valid");
                //errorTXT.text = Fa.faConvert("Ad is Not valid");
                //ErrorPanel.SetActive(true);
            }
        }
    }

    private void requestAd(string zone, bool cached)
    {
        Tapsell.requestAd(zone, cached,
            (TapsellAd result) => {
                // onAdAvailable
                Debug.Log("Action: onAdAvailable");
                TapsellAdVideoPlay.available = true;
                TapsellAdVideoPlay.ad = result;


                // Play Ad
                if (TapsellAdVideoPlay.available)
                {
                    TapsellAdVideoPlay.available = false;
                    TapsellShowOptions options = new TapsellShowOptions();
                    options.backDisabled = true;
                    options.immersiveMode = false;
                    options.rotationMode = TapsellShowOptions.ROTATION_LOCKED_LANDSCAPE;
                    options.showDialog = true;
                    Tapsell.showAd(ad, options);
                }
            },

            (string zoneId) => {
                // onNoAdAvailable
                Debug.Log("No Ad Available");

                errorTXT.text = Fa.faConvert("تبلیغی برای نمایش وجود ندارد.");
                ErrorPanel.SetActive(true);
            },

            (TapsellError error) => {
                // onError
                Debug.Log(error.error);



                TapsellAdVideoPlay.available = false;
                TapsellAdVideoPlay.ad = null;
                
                //errorTXT.text = Fa.faConvert("مشکلی بوحود آمد، لطفا دوباره تلاش نمایید.") + error.error;
                //ErrorPanel.SetActive(true);
            },

            (string zoneId) => {
                // onNoNetwork
                Debug.Log("No Network: " + zoneId);

                errorTXT.text = Fa.faConvert("مشکل در ارتباط با اینترنت.");
                ErrorPanel.SetActive(true);
            },

            (TapsellAd result) => {
                //onExpiring
                Debug.Log("Expiring");
                TapsellAdVideoPlay.available = false;
                TapsellAdVideoPlay.ad = null;
                requestAd(result.zoneId, false);

                //errorTXT.text = Fa.faConvert("زمانی که تبلیغ منقضی شو Expiring");
                //ErrorPanel.SetActive(true);
            }

        );
    }

    public void OnRequestVideoAdClick() // Step 1 Click the video Ad BTN in Shop
    {
        GameAnalytics.NewDesignEvent("Ad Click");

        requestAd(rewardAdId, false);   //request video & start Dowloading
    }
}
