using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public int weaponID;
    public Text buttonText;
    public Text useButtonText;
    public GameObject useButtonOBJ;
    public Text weaponLevelText;

    public void BuyWeapon() //ON Click
    {
        if (weaponID == 0)
        {
            return;
        }

        for (int i = 0; i < WeaponShop.weaponShop.weaponList.Count; i++)
        {
            if (WeaponShop.weaponShop.weaponList[i].weaponID == weaponID
               && !WeaponShop.weaponShop.weaponList[i].bought)
            {
                if (UpgradeSystem.upgradeSystem.RequestCoin(WeaponShop.weaponShop.weaponList[i].weaponPrice))
                {
                    // if mony is Enogh Buy The Weapon
                    WeaponShop.weaponShop.weaponList[i].bought = true;
                    WeaponShop.weaponShop.weaponList[i].upgradeLevel = 1;
                    UpgradeSystem.upgradeSystem.RediuceCoin(WeaponShop.weaponShop.weaponList[i].weaponPrice);


                    //Active Use BTN
                    useButtonOBJ.SetActive(true);

                    //change buy Button TXT to Upgrade + New Price
                    WeaponShop.weaponShop.weaponList[i].weaponPrice += Mathf.RoundToInt(WeaponShop.weaponShop.weaponList[i].weaponPrice / 10);

                    UpdateBuyBuutton();
                }
                else if (!UpgradeSystem.upgradeSystem.RequestCoin(WeaponShop.weaponShop.weaponList[i].weaponPrice)) 
                {
                    WeaponShop.weaponShop.ShowWarning(); //Show Error Mony Not Enogh
                }
            }
            else if (WeaponShop.weaponShop.weaponList[i].weaponID == weaponID
                  && WeaponShop.weaponShop.weaponList[i].bought)
            {
                if (UpgradeSystem.upgradeSystem.RequestCoin(WeaponShop.weaponShop.weaponList[i].weaponPrice))
                {
                    // If Weapon Bought Buy new Upgrade & Update Data
                    WeaponShop.weaponShop.weaponList[i].upgradeLevel++;
                    
                    UpgradeSystem.upgradeSystem.RediuceCoin(WeaponShop.weaponShop.weaponList[i].weaponPrice);

                    WeaponShop.weaponShop.weaponList[i].weaponPrice += Mathf.RoundToInt(WeaponShop.weaponShop.weaponList[i].weaponPrice / 10);
                    
                    WeaponShop.weaponShop.weaponList[i].damage += Mathf.RoundToInt(WeaponShop.weaponShop.weaponList[i].damage / 10);

                    if(WeaponShop.weaponShop.weaponList[i].fireRate > 0.1f)
                    {
                        WeaponShop.weaponShop.weaponList[i].fireRate -= (WeaponShop.weaponShop.weaponList[i].fireRate / 20);
                    }
                    

                    UpdateBuyBuutton();
                }
                else if (!UpgradeSystem.upgradeSystem.RequestCoin(WeaponShop.weaponShop.weaponList[i].weaponPrice))
                {
                    WeaponShop.weaponShop.ShowWarning(); //Show Error Mony Not Enogh
                }
            }
        }
    }

    void UpdateBuyBuutton()
    {
        WeaponShop.weaponShop.UpdateBuyButtonAfterLoad();
        WeaponShop.weaponShop.UpdateSprite(weaponID);

        // Save After Buy Weapon
        SaveLoad.saveLoad.Saving();
    }

    public void UpdateUseBuutton()
    {
        UpgradeSystem.upgradeSystem.currentWeaponID = weaponID;
        WeaponShop.weaponShop.UpdateUseButtonAfterLoad();

        // Save After Buy Weapon
        SaveLoad.saveLoad.Saving();
    }
}
