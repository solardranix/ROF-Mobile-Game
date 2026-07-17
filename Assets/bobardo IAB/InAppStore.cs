using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;

/* Apache License. Copyright (C) Bobardo Studio - All Rights Reserved.
 * Unauthorized publishing the plugin with different name is strictly prohibited.
 * This plugin is free and no one has right to sell it to others.
 * http://bobardo.com
 * http://opensource.org/licenses/Apache-2.0
 */

[RequireComponent(typeof(StoreHandler))]
public class InAppStore : MonoBehaviour
{
    public Product[] products;

    private int selectedProductIndex;

    public GameObject errorPanel;
    public Text errorText;

    public void purchasedSuccessful(Purchase purchase)
    {
        // purchase was successful, give user the pruduct
        
 
        switch (selectedProductIndex)
        {
            
            case 0:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 1000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();

                    //errorText.text = "Index is = " + selectedProductIndex;
                    errorText.text = Fa.faConvert("1000 سکه به سکه های شما اضافه شد.");
                    errorPanel.SetActive(true);

                    GameAnalytics.NewBusinessEvent("USD", 16, "Coin Pack", "SmallPackage", "Coin Shop");
                }

                break;

            case 1:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 3000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();

                    errorText.text = Fa.faConvert("3000 سکه به سکه های شما اضافه شد.");
                    errorPanel.SetActive(true);

                    GameAnalytics.NewBusinessEvent("USD", 45, "Coin Pack", "NormalPackage", "Coin Shop");
                }

                break;

            case 2:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 7000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();

                    errorText.text = Fa.faConvert("7000 سکه به سکه های شما اضافه شد.");
                    errorPanel.SetActive(true);

                    GameAnalytics.NewBusinessEvent("USD", 100, "Coin Pack", "AveragePackage", "Coin Shop");
                }

                break;

            case 3:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 17000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();

                    errorText.text = Fa.faConvert("17000 سکه به سکه های شما اضافه شد.");
                    errorPanel.SetActive(true);

                    GameAnalytics.NewBusinessEvent("USD", 230, "Coin Pack", "BigPackage", "Coin Shop");
                }

                break;

            case 4:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 40000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();

                    errorText.text = Fa.faConvert("40000 سکه به سکه های شما اضافه شد.");
                    errorPanel.SetActive(true);

                    GameAnalytics.NewBusinessEvent("USD", 480, "Coin Pack", "SpecialPackage", "Coin Shop");
                }

                break; 

            default:
                throw new UnassignedReferenceException("you forgot to give user the product after purchase. product: " + purchase.productId);
        }
    }

    public void purchasedFailed(int errorCode, string info)
    {
        // purchase failed. show user the proper message
        switch (errorCode)
        {
            case 1: // error connecting cafeBazaar
            case 2: // error connecting cafeBazaar
            case 4: // error connecting cafeBazaar
            case 5: // error connecting cafeBazaar
                {
                    errorText.text = Fa.faConvert("مشکل در ارتباط با کافه بازار");
                    errorPanel.SetActive(true);
                }
                break;
            case 6: // user canceled the purchase
                {
                    errorText.text = Fa.faConvert("خطایی در مراحل خرید رخ داده است");
                    errorPanel.SetActive(true);
                }
                break;
            case 7: // purchase failed
                {
                    errorText.text = Fa.faConvert("خطایی در مراحل خرید رخ داده است");
                    errorPanel.SetActive(true);
                }
                break;
            case 8: // failed to consume product. but the purchase was successful.
                {
                    errorText.text = Fa.faConvert("خرید موفقیت آمیز بوده اما به دلیل داشتن آیتم اعمال نشده است");
                    errorPanel.SetActive(true);
                }
                break;
            case 12: // error setup cafebazaar billing
            case 13: // error setup cafebazaar billing
            case 14: // error setup cafebazaar billing
                {
                    errorText.text = Fa.faConvert("خطایی در ارتباط با کافه بازار رخ داده.");
                    errorPanel.SetActive(true);
                }
                break;
            case 15: // you should enter your public key
                {
                    errorText.text = Fa.faConvert("شما فراموش کردین مقدار public key رو در پروژه قرار بدید.");
                    errorPanel.SetActive(true);
                }
                break;
            case 16: // unkown error happened
                {
                    errorText.text = Fa.faConvert("خطای ناشناخته رخ داده.");
                    errorPanel.SetActive(true);
                }
                break;
            case 17: // the result from cafeBazaar is not valid.
                {
                    errorText.text = Fa.faConvert("مقادیر برگشتی از سمت کافه بازار نامعتبره.");
                    errorPanel.SetActive(true);
                }
                break;
        }

    }

    public void userHasThisProduct(Purchase purchase)
    {
        // user already has this product
        
        switch (selectedProductIndex)
        {
            case 0:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 1000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();
                }

                break;

            case 1:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 3000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();
                }

                break;

            case 2:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 7000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();
                }

                break;

            case 3:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 17000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();
                }

                break;

            case 4:
                {
                    UpgradeSystem.upgradeSystem.SetCoinInfo(UpgradeSystem.upgradeSystem.GetCoinInfo() + 40000);
                    // Save After Buy Coin
                    SaveLoad.saveLoad.Saving();
                }

                break;
            default:
                throw new UnassignedReferenceException("you forgot to give user the product after purchase. product: " + purchase.productId);
        }
        
    }

    public void failToGetUserInventory(int errorCode, string info)
    {
        // user has not this product or some error happened
        switch (errorCode)
        {
            case 3:  // error connecting cafeBazaar
            case 10: // error connecting cafeBazaar

                break;
            case 9: // user didn't login to cafeBazaar

                break;
            case 11: // user has not this product

                break;
            case 12: // error setup cafebazaar billing
            case 13: // error setup cafebazaar billing
            case 14: // error setup cafebazaar billing

                break;
            case 15: // you should enter your public key

                break;
            case 16: // unkown error happened

                break;
            case 17: // the result from cafeBazaar is not valid.

                break;
        }

    }

    public void purchaseProduct(int productIndex)
    {
        selectedProductIndex = productIndex;
        Product product = products[productIndex];
        if (product.type == Product.ProductType.Consumable)
        {
            GetComponent<StoreHandler>().BuyAndConsume(product.productId);
        }
        else if (product.type == Product.ProductType.NonConsumable)
        {
            GetComponent<StoreHandler>().BuyProduct(product.productId);
        }
    }

    public void checkIfUserHasProduct(int productIndex)
    {
        selectedProductIndex = productIndex;
        GetComponent<StoreHandler>().CheckInventory(products[productIndex].productId);
    }

    
}

