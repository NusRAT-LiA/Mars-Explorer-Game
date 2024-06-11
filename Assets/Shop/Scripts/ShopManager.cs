using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ShopItemSO[] shopItemSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseButton;
    private TextMeshProUGUI buttonText;
    public PlayerController playerController;
    public ShovelController shovelController;

    void Start()
    {
        LoadState();

        for (int i = 0; i < shopPanelsGO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
            LoadPanels();
        }

        coinUI.text = "Basalts: " + coins.ToString();
        CheckPurchaseable();
    }

    void Update()
    {
    }

    public void AddCoins()
    {
        coins++;
        coinUI.text = "Basalts: " + coins.ToString();
        CheckPurchaseable();
        SaveState();
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            buttonText = myPurchaseButton[i].GetComponentInChildren<TextMeshProUGUI>();
            if (coins >= shopItemSO[i].baseCost || buttonText.text == "Equip")
                myPurchaseButton[i].interactable = true;
            else
                myPurchaseButton[i].interactable = false;
        }
    }

    public void PurchaseItem(int btnNo)
    {
        buttonText = myPurchaseButton[btnNo].GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text == "Purchase")
        {
            if (coins >= shopItemSO[btnNo].baseCost)
            {
                buttonText.text = "Equip";
                coins -= shopItemSO[btnNo].baseCost;
                coinUI.text = "Basalts: " + coins.ToString();
                SaveState();
            }
        }
        else if (buttonText.text == "Equip")
        {
            myPurchaseButton[btnNo].interactable = true;
            if (shopItemSO[btnNo].type == "Suit")
            {
                if (playerController != null)
                {
                    playerController.moveSpeed = shopItemSO[btnNo].speed;
                }
            }
            else if (shopItemSO[btnNo].type == "Shovel")
            {
                shovelController.ActivateChildGameObjectByName(shopItemSO[btnNo].name);
            }
            SaveState();
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleText.text = shopItemSO[i].title;
            shopPanels[i].descriptionText.text = shopItemSO[i].description;
            shopPanels[i].costText.text = "Basalts: " + shopItemSO[i].baseCost.ToString();
        }
    }

    private void SaveState()
    {
        PlayerPrefs.SetInt("Coins", coins);
        for (int i = 0; i < myPurchaseButton.Length; i++)
        {
            buttonText = myPurchaseButton[i].GetComponentInChildren<TextMeshProUGUI>();
            PlayerPrefs.SetString("Button" + i, buttonText.text);
        }
        PlayerPrefs.Save();
    }

    private void LoadState()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        for (int i = 0; i < myPurchaseButton.Length; i++)
        {
            buttonText = myPurchaseButton[i].GetComponentInChildren<TextMeshProUGUI>();
            if (PlayerPrefs.HasKey("Button" + i))
            {
                buttonText.text = PlayerPrefs.GetString("Button" + i);
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Coins");
        for (int i = 0; i < myPurchaseButton.Length; i++)
        {
            PlayerPrefs.DeleteKey("Button" + i);
        }
        PlayerPrefs.Save();
    }
}
