using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//For Using Binary Saving
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GameAnalyticsSDK;


public class SaveLoad : MonoBehaviour
{
    public static SaveLoad saveLoad;

    [System.Serializable]
    public class SaveData
    {
        public List<Weapon> shopListForSave = new List<Weapon>();
        public string userName;
        public int coin;
        public int currentWeaponID;
        public int bestScore;
        //public int newScore;
    }

    void Awake()
    {
        if (saveLoad == null)
        {
            DontDestroyOnLoad(gameObject);
            saveLoad = this;
        }
        else if (saveLoad != this)
        {
            Destroy(gameObject);
        }
    }

    public void Saving()
    {
        SaveData data = new SaveData();
        data.userName = UpgradeSystem.upgradeSystem.GetUserNameInfo();
        data.coin = UpgradeSystem.upgradeSystem.GetCoinInfo();
        data.bestScore = UpgradeSystem.upgradeSystem.GetScoreInfo();
        data.currentWeaponID = UpgradeSystem.upgradeSystem.currentWeaponID;

        // Add All Weapons From the WeaponShop List
        for(int i = 0; i < WeaponShop.weaponShop.weaponList.Count; i++)
        {
            data.shopListForSave.Add(WeaponShop.weaponShop.weaponList[i]);
        }

        BinaryFormatter bFormatter = new BinaryFormatter();
        // This Also work at Mobile But This Not working on Web
        FileStream stream = new FileStream(Application.persistentDataPath + "/shop.dr", FileMode.Create);

        bFormatter.Serialize(stream, data);
        stream.Close();
    }

    public void Loading()
    {
        if (File.Exists(Application.persistentDataPath + "/shop.dr"))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/shop.dr", FileMode.Open);
            
            SaveData data = (SaveData)bFormatter.Deserialize(stream);

            UpgradeSystem.upgradeSystem.SetUserNameInfo(data.userName);
            UpgradeSystem.upgradeSystem.SetCoinInfo(data.coin);
            UpgradeSystem.upgradeSystem.SetScoreInfo(data.bestScore);
            UpgradeSystem.upgradeSystem.currentWeaponID = data.currentWeaponID;

            stream.Close();

            for(int i = 0; i < data.shopListForSave.Count; i++)
            {
                // Update the shop
                WeaponShop.weaponShop.weaponList[i] = data.shopListForSave[i];
                // Update All Sprites
                WeaponShop.weaponShop.UpdateSprite(WeaponShop.weaponShop.weaponList[i].weaponID);
                // Update All Buttons
                WeaponShop.weaponShop.UpdateBuyButtonAfterLoad();
                WeaponShop.weaponShop.UpdateUseButtonAfterLoad();
            }
        }
        else
        {
            //WeaponShop.weaponShop.weaponList[i] = data.shopListForSave[i];
            // Update All Sprites
            WeaponShop.weaponShop.UpdateSprite(WeaponShop.weaponShop.weaponList[1].weaponID);
            // Update All Buttons
            WeaponShop.weaponShop.UpdateBuyButtonAfterLoad();
            WeaponShop.weaponShop.UpdateUseButtonAfterLoad();
        }
    }



    public void LevelSaving(int newBestScore)
    {
        SaveData data = new SaveData();
        data.userName = GameManager.gameManager.userName;
        data.coin = GameManager.gameManager.coin;
        data.bestScore = newBestScore;
        data.currentWeaponID = UpgradeSystem.upgradeSystem.currentWeaponID;

        // Add All Weapons From the WeaponShop List
        for (int i = 0; i < WeaponShop.weaponShop.weaponList.Count; i++)
        {
            data.shopListForSave.Add(WeaponShop.weaponShop.weaponList[i]);
        }

        BinaryFormatter bFormatter = new BinaryFormatter();
        // This Also work at Mobile But This Not working on Web
        FileStream stream = new FileStream(Application.persistentDataPath + "/shop.dr", FileMode.Create);

        bFormatter.Serialize(stream, data);
        stream.Close();

        HSController.Instance.startPostScores(newBestScore);
    }

    public void LevelWeaponLoading()
    {
        if (File.Exists(Application.persistentDataPath + "/shop.dr"))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/shop.dr", FileMode.Open);

            SaveData data = (SaveData)bFormatter.Deserialize(stream);
            stream.Close();


            //Debug.Log(data.coin + "   " + data.bestScore);

            WeaponInit weaponInitialize = GameObject.FindGameObjectWithTag("GM").GetComponent<WeaponInit>();

            weaponInitialize.InitScoreAndCoin(data.userName, 
                                              data.bestScore,
                                              data.coin);

            for (int i = 0; i < data.shopListForSave.Count; i++)
            {
                if (data.shopListForSave[i].weaponID == data.currentWeaponID)
                {
                    //Debug.Log(weaponInitialize.Test);

                    //Debug.Log(data.shopListForSave[i].bought);
                    //Debug.Log(data.shopListForSave[i].bulletNumInPool);
                    //Debug.Log(data.shopListForSave[i].fireRate);
                    //Debug.Log(data.shopListForSave[i].spriteName);
                    //Debug.Log(data.shopListForSave[i].weaponID);
                    //Debug.Log(data.shopListForSave[i].weaponName);
                    //Debug.Log(data.shopListForSave[i].weaponPrice);



                    weaponInitialize.InitLevelWeapon(data.shopListForSave[i].bulletNumInPool
                                                   , data.shopListForSave[i].damage
                                                   , data.shopListForSave[i].fireRate
                                                   , data.shopListForSave[i].spawnerCount
                                                   , data.shopListForSave[i].spriteName);

                    string weaponForGA = "Weapon:" + data.shopListForSave[i].spriteName;
                    GameAnalytics.NewDesignEvent(weaponForGA);
                    Debug.Log(weaponForGA);

                }
            }
            
        }
    }


    public void DeleteSaveData()
    {
        if(File.Exists(Application.persistentDataPath + "/shop.dr"))
        {
            File.Delete(Application.persistentDataPath + "/shop.dr");
        }

        if (File.Exists(Application.persistentDataPath + "/curWe.dr"))
        {
            File.Delete(Application.persistentDataPath + "/curWe.dr");
        
        }
        Debug.Log("Dellllllllllll");
    }
}
