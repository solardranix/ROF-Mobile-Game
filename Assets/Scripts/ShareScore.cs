using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameAnalyticsSDK;

public class ShareScore : MonoBehaviour
{
    
    public GameObject canvasShareObj;
    public static int myBestScore = 0;
    private bool isProcessing = false;
    private bool isFocus = false;

	public void ShareBtnPress()
    {
        if(!isProcessing)
        {
            canvasShareObj.SetActive(true);
            StartCoroutine(ShareScreenshot());
        }
    }

    IEnumerator ShareScreenshot()
    {
        isProcessing = true;

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot("screenshot.png", 1);
        string destination = Path.Combine(Application.persistentDataPath, "screenshot.png");

        yield return new WaitForSecondsRealtime(0.3f);

        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
                uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
                "رکورد من در بازی آتش باران :" + myBestScore + ". اگه فکر می کنی میتونی بیشتر از من رکورد بزنی بازی آتش باران رو از کافه بازار دانلود کن. لینک بازی: https://cafebazaar.ir/app/com.TilehPouya.RainOfFire/?l=fa ");
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
                intentObject, "Share your new score");
            currentActivity.Call("startActivity", chooser);

            string shareTextForDesignEvent = "Score Shared In " + chooser;
            GameAnalytics.NewDesignEvent(shareTextForDesignEvent);

            yield return new WaitForSecondsRealtime(1);
        }

        yield return new WaitUntil(() => isFocus);
        canvasShareObj.SetActive(false);
        isProcessing = false;

        
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
    
}
