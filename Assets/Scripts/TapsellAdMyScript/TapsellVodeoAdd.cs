using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using TapsellSDK;
using TapsellSimpleJSON;
using ArabicSupport;

public class TapsellVodeoAdd : MonoBehaviour
{
    public static bool available = false;
    public static TapsellAd ad = null;
    public static TapsellNativeBannerAd nativeAd = null;

    public const string AdId = "5a342adc799e6f000146f937";


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

        //Tapsell.requestBannerAd(AdId, BannerType.BANNER_320x50, Gravity.BOTTOM, Gravity.CENTER);
    }


    public void VideoAdRequestBTN() // Step 1 Click the video Ad BTN in Shop
    {
        requestAd(AdId, false);
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
            errorTXT.text = "my server error is " + data.error;
            ErrorPanel.SetActive(true);
        }
        else
        {
            Debug.Log("my server result is " + data.text);

            JSONNode node = JSON.Parse(data.text);
            bool valid = node["valid"].AsBool;
            if (valid)
            {
                // Add Coin To User
                UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 50);
                // Save After Buy Coin
                SaveLoad.saveLoad.Saving();
                // if suggestion is valid, you can give in game gifts to the user
                Debug.Log("Ad is valid");
                
                ErrorPanel.SetActive(true);
                errorTXT.text = "Add 50 Coin";
            }
            else {
                Debug.Log("Ad is not valid");
            }
        }
    }

    private void requestAd(string zone, bool cached) //Step 2 Request for Ad Download
    {
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
            },

            (TapsellError error) => {
                // onError
                Debug.Log(error.error); //هنگامی که هر نوع خطایی در پروسه‌ی دریافت تبلیغ بوجود بیاید
            },

            (string zoneId) => {
                // onNoNetwork
                Debug.Log("No Network: " + zoneId); //زمانی که دسترسی به شبکه موجود نباشد.
            },

            (TapsellAd result) => {
                //onExpiring
                Debug.Log("Expiring"); //زمانی که تبلیغ منقضی شود
                TapsellVodeoAdd.available = false;
                TapsellVodeoAdd.ad = null;
                requestAd(result.zoneId, false); //درخواست جدید دانلود تبلیغ
            }

        );

        //Show Ad After Download
        if (TapsellVodeoAdd.available) //if video Ad downloaded Then show ad
        {
            TapsellVodeoAdd.available = false;
            TapsellShowOptions options = new TapsellShowOptions();
            options.backDisabled = true;
            options.immersiveMode = false;
            options.rotationMode = TapsellShowOptions.ROTATION_LOCKED_LANDSCAPE;
            options.showDialog = true;
            Tapsell.showAd(ad, options);
        }
    }

/*
    void OnGUI()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
		if(TapsellVodeoAdd.nativeAd==null)
		{
			if(GUI.Button(new Rect(50, 150, 200, 100), "Request Banner Ad")){
				requestNativeBannerAd (AdId);
			}
		}


		if(TapsellVodeoAdd.nativeAd!=null)
		{
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.alignment = TextAnchor.UpperRight;
			GUI.Label (new Rect (50, 250, 450, 30), ArabicFixer.Fix(TapsellVodeoAdd.nativeAd.getTitle (),true), titleStyle);
			
			GUIStyle descriptionStyle = new GUIStyle ();
			descriptionStyle.richText = true;
			descriptionStyle.alignment = TextAnchor.MiddleRight;
			GUI.Label (new Rect (50, 280, 450, 20), ArabicFixer.Fix(TapsellVodeoAdd.nativeAd.getDescription (),true), descriptionStyle);
			GUI.DrawTexture (new Rect(500, 250, 50, 50), TapsellVodeoAdd.nativeAd.getIcon() );
			Rect callToActionRect;
			if(TapsellVodeoAdd.nativeAd.getLandscapeBannerImage()!=null)
			{
				GUI.DrawTexture (new Rect(50, 300, 500, 280), TapsellVodeoAdd.nativeAd.getLandscapeBannerImage() );
				callToActionRect = new Rect(50, 580, 500, 50);
			}
			else if(TapsellVodeoAdd.nativeAd.getPortraitBannerImage()!=null)
			{
				GUI.DrawTexture (new Rect(50, 300, 500, 280), TapsellVodeoAdd.nativeAd.getPortraitBannerImage() );
				callToActionRect = new Rect(50, 580, 500, 50);
			}
			else
			{
				callToActionRect = new Rect(50, 300, 500, 50);
			}
		    TapsellVodeoAdd.nativeAd.onShown ();
			if(GUI.Button (callToActionRect, ArabicFixer.Fix(TapsellVodeoAdd.nativeAd.getCallToAction (),true) ))
			{
				TapsellVodeoAdd.nativeAd.onClicked ();
			}
		}
#endif

    }

    private void requestNativeBannerAd(string zone)
    {
        Tapsell.requestNativeBannerAd(this, zone,
            (TapsellNativeBannerAd result) => {
                // onAdAvailable
                Debug.Log("Action: onNativeRequestFilled");

                TapsellVodeoAdd.nativeAd = result;

            },

            (string zoneId) => {
                // onNoAdAvailable
                Debug.Log("No Ad Available");
            },

            (TapsellError error) => {
                // onError
                Debug.Log(error.error);
            },

            (string zoneId) => {
                // onNoNetwork
                Debug.Log("No Network: " + zoneId);
            }
        );
    }
    */

    
}
