using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public static WeaponShop weaponShop;

    public string buyBTNPersianText;
    public string UpgradeBTNPersianText;
    public string useBTNPersianText;
    public string usingBTNPersianText;
    public string weaponLevelPersianText;

    public List<Weapon> weaponList = new List<Weapon>();

    private List<GameObject> itemHolderList = new List<GameObject>();

    public List<GameObject> buyButtonList = new List<GameObject>();

    public List<GameObject> useButtonList = new List<GameObject>();

    public GameObject itemHolderPrefab;
    public Transform grid;

    public GameObject warningImage;

    // Use this for initialization
    void Start()
    {
        warningImage.SetActive(false);
        weaponShop = this;
        FillList();

    }

    void FillList()
    {

        //for(int i = 0; i < weaponList.Count; i++)
        for (int i = 0; i < weaponList.Count; i++)
        {
            // Handle List
            itemHolderList.Add(Instantiate(itemHolderPrefab, grid, false));

            ItemHolder holderScript = itemHolderList[i].GetComponent<ItemHolder>();

            holderScript.itemID = weaponList[i].weaponID;
            holderScript.itemName.text = weaponList[i].weaponName;
            //holderScript.itemPrice.text = weaponList[i].weaponPrice.ToString();

            // BUY BUTTON
            holderScript.buyButton.GetComponent<BuyButton>().weaponID = weaponList[i].weaponID;
            holderScript.buyButton.GetComponent<BuyButton>().buttonText.text = weaponList[i].weaponPrice.ToString() + " " + buyBTNPersianText;
            

            // Handle Lists For Buybutton
            buyButtonList.Add(holderScript.buyButton);

            // Handle Lists For Buybutton
            useButtonList.Add(holderScript.useButton);


            if (weaponList[i].bought)
            {
                holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + weaponList[i].spriteName);
                holderScript.buyButton.GetComponent<BuyButton>().useButtonOBJ.SetActive(true);
            }
            else
            {
                holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + weaponList[i].spriteName + "_Dark");
            }
        }
    }

    public void UpdateSprite(int weaponID)
    {
        for (int i = 0; i < itemHolderList.Count; i++)
        {
            ItemHolder holderScript = itemHolderList[i].GetComponent<ItemHolder>();
            if (holderScript.itemID == weaponID)
            {
                for (int j = 0; j < weaponList.Count; j++)
                {
                    if (weaponList[j].weaponID == weaponID)
                    {
                        if (weaponList[j].bought)
                        {
                            if (weaponList[i].bought)
                            {
                                holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + weaponList[j].spriteName);
                                holderScript.buyButton.GetComponent<BuyButton>().useButtonOBJ.SetActive(true);
                            }
                            else
                            {
                                holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + weaponList[j].spriteName + "_Dark");
                            }
                        }
                    }
                }
            }
        }
    }

    public void UpdateBuyButtonAfterLoad()
    {
        int currentWeaponID = UpgradeSystem.upgradeSystem.currentWeaponID;

        for(int i = 0; i<buyButtonList.Count; i++)
        {
            BuyButton buyButtonScript = buyButtonList[i].GetComponent<BuyButton>();
            for(int j = 0; j < weaponList.Count; j++)
            {
                if(weaponList[j].weaponID == buyButtonScript.weaponID
                   && weaponList[j].bought)
                {
                    buyButtonScript.buttonText.text = weaponList[j].weaponPrice.ToString() + " " + UpgradeBTNPersianText;
                    buyButtonScript.weaponLevelText.text = weaponList[j].upgradeLevel.ToString() + " " + weaponLevelPersianText;
                }
            }
        }
    }

    public void UpdateUseButtonAfterLoad()
    {
        int currentWeaponID = UpgradeSystem.upgradeSystem.currentWeaponID;

        for (int i = 0; i < useButtonList.Count; i++)
        {
            BuyButton buyButtonScript = buyButtonList[i].GetComponent<BuyButton>();
            for (int j = 0; j < weaponList.Count; j++)
            {
                if (weaponList[j].weaponID == buyButtonScript.weaponID
                   && weaponList[j].bought)
                {
                    if (weaponList[j].weaponID != currentWeaponID)
                    {
                        buyButtonScript.useButtonText.text = useBTNPersianText;
                    }
                    else if (weaponList[j].weaponID == currentWeaponID)
                    {
                        buyButtonScript.useButtonText.text = usingBTNPersianText;
                    }
                }
            }
        }
    }

    public void ShowWarning()
    {
        warningImage.SetActive(true);
    }
}
