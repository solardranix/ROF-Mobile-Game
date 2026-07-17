using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    //--------- UI & Value ----------------------
    [SerializeField]
    private Text timeTXT;

    [SerializeField]
    private Text coinTXT;
    [SerializeField]
    private Text scoreTXT;
    [SerializeField]
    private Slider health;
    [SerializeField]
    private Text comboTXT;
    [SerializeField]
    private Text comboCoinTXT;

    //--------- Score Panel Value ----------------------
    [SerializeField]
    private Text bestScoreTXTInPanel;
    [SerializeField]
    private Text scoreTXTInPanel;
    [SerializeField]
    private Text restorButtonTXTInPanel;

    //-------------------------- Singelton Pattern --------------------------
    public static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("UIManager");
                    _instance = container.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }
    //-----------------------------------------------------------------------
    public void UpdateCoin(int coin)
    {
        coinTXT.text = coin.ToString();
    }

    public void UpdateCoinReward(int coin)
    {
        comboCoinTXT.text = coin.ToString();
    }

    public void UpdateTime(int Min, float Sec)
    {
        timeTXT.text = "Time : " + Min.ToString() + ":" + Sec.ToString("f0");
    }

    public void UpdateScore(int score)
    {
        scoreTXT.text = score.ToString() + " " + Fa.faConvert("امتیاز: ");
    }
    /*
    public void UpdateCombo(int comboCount)
    {
        if (comboCount > 0)
        {
            comboTXT.text = comboCount.ToString() +" " + Fa.faConvert("ترکاندن با فاصله کم: ");
        }
        else
        {
            comboTXT.text = "";
        }
    }
    */
    public void UpdateHealth(float dama)
    {
        health.value -= dama; 
    }

    public void RestoreHealth()
    {
        health.value = 1.0f;
    }

    public void ScorePanelInit(int score, int bestScore, int restorePrice)
    {
        scoreTXTInPanel.text = score.ToString() + " " + Fa.faConvert("رکورد جدید: ");
        bestScoreTXTInPanel.text = bestScore.ToString() + " " + Fa.faConvert("بهترین رکورد: ");

        restorButtonTXTInPanel.text = restorePrice.ToString();
    }


}