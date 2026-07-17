using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using TapsellSDK;
using TapsellSimpleJSON;
using ArabicSupport;

public class TapsellRewardAdBTN : MonoBehaviour
{
    public static bool available = false;
    public static TapsellAd ad = null;
    public static TapsellNativeBannerAd nativeAd = null;

    public const string AdId = "5a342adc799e6f000146f937";



    //UI
    public GameObject adLoadingPanel;
    public GameObject adPlayBTN;
    public Text errorTXT;
    public GameObject ErrorPanel;

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
                    validateSuggestion(result.adId);
                }
            }
        );
        Tapsell.requestBannerAd(AdId, BannerType.BANNER_320x50, Gravity.BOTTOM, Gravity.CENTER);
    }

    public void VideoAdRequestBTN() // Step 1 Click the video Ad BTN in Shop
    {
        requestAd(AdId, false);
    }

    private void requestAd(string zone, bool cached) //Step 2 Request for Ad Download
    {
        adLoadingPanel.SetActive(true);
        Tapsell.requestAd(zone, cached,
            (TapsellAd result) => {
                // onAdAvailable
                Debug.Log("Action: onAdAvailable");
                TapsellVodeoAdd.available = true; //زمانی که تبلیغ دریافت شده و آماده‌ی نمایش باشد.
                TapsellVodeoAdd.ad = result; //ذخیره تبلیغ دانلود شده در متغیر ad
            },

            (string zoneId) => {
                // onNoAdAvailable
                Debug.Log("No Ad Available"); //تبلیغی برای نمایش وجود ندارد
                adLoadingPanel.SetActive(false);
                errorTXT.text = "تبلیغی برای نمایش وجود ندارد No Ad Available";
                ErrorPanel.SetActive(true);
            },

            (TapsellError error) => {
                // onError
                Debug.Log(error.error); //هنگامی که هر نوع خطایی در پروسه‌ی دریافت تبلیغ بوجود بیاید
                adLoadingPanel.SetActive(false);
                errorTXT.text = "هنگامی که هر نوع خطایی در پروسه‌ی دریافت تبلیغ بوجود بیاید " + error.error;
                ErrorPanel.SetActive(true);
            },

            (string zoneId) => {
                // onNoNetwork
                Debug.Log("No Network: " + zoneId); //زمانی که دسترسی به شبکه موجود نباشد.
                adLoadingPanel.SetActive(false);
                errorTXT.text = "زمانی که دسترسی به شبکه موجود نباشد No Network";
                ErrorPanel.SetActive(true);
            },

            (TapsellAd result) => {
                //onExpiring
                Debug.Log("Expiring"); //زمانی که تبلیغ منقضی شود
                TapsellVodeoAdd.available = false;
                TapsellVodeoAdd.ad = null;
                requestAd(result.zoneId, false); //درخواست جدید دانلود تبلیغ
                adLoadingPanel.SetActive(false);
                errorTXT.text = "زمانی که تبلیغ منقضی شو Expiring";
                ErrorPanel.SetActive(true);
            }

        );

        //Show Ad After Download
        if (TapsellVodeoAdd.available) //if video Ad downloaded Then show ad
        {
            adPlayBTN.SetActive(true);
        }
    }

    public void OnAdPlayClickBTN()
    {
        adLoadingPanel.SetActive(false);
        adPlayBTN.SetActive(false);
        TapsellVodeoAdd.available = false;
        //play setting
        TapsellShowOptions options = new TapsellShowOptions();
        options.backDisabled = true;
        options.immersiveMode = false;
        options.rotationMode = TapsellShowOptions.ROTATION_LOCKED_LANDSCAPE;
        options.showDialog = true;
        //play
        Tapsell.showAd(ad, options);
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
                // Add Coin To User
                UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 50);
                // Save After Buy Coin
                SaveLoad.saveLoad.Saving();
            }
            else {
                Debug.Log("Ad is not valid");
            }
        }
    }
}
