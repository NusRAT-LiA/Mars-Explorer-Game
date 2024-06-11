using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public int coins;
    public TMP_Text coinUI;
    public ShopItemSO[] shopItemSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseButton;
    private TextMeshProUGUI buttonText;
    public PlayerController playerController;

    public ShovelController shovelController;

    // Start is called before the first frame update
    void Start()
    {
         SceneManager.sceneLoaded += OnSceneLoaded;
        for (int i = 0; i < 9; i++)
        {
            shopPanelsGO[i].SetActive(true);
            coinUI.text = "Basalts: " + coins.ToString();
            LoadPanels();
            CheckPurchaseable();
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUI();
    }

    void Update()
    {

    }

    public void UpdateUI()
    {
        if (coinUI != null)
        {
            coinUI.text = "Basalts: " + coins.ToString();
        }
        LoadPanels();
        CheckPurchaseable();
    }

    public void AddCoins()
    {
        coins++;
        coinUI.text = "Basalts: " + coins.ToString();
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < 9; i++)
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
                coins = coins - shopItemSO[btnNo].baseCost;
                coinUI.text = "Basalts: " + coins.ToString();
                // CheckPurchaseable();
            }
        }
        else if (buttonText.text == "Equip")
        {
            myPurchaseButton[btnNo].interactable = true;
            if (shopItemSO[btnNo].type == "Suit")
            {
                Debug.Log(shopItemSO[btnNo].speed + "   " + shopItemSO[btnNo].jumpForce);
                if (playerController != null)
                {
                    Debug.Log(playerController.moveSpeed);
                    playerController.moveSpeed = shopItemSO[btnNo].speed;
                    Debug.Log(playerController.moveSpeed);
                }
            }
            else if (shopItemSO[btnNo].type == "Shovel")
            {
                Debug.Log(shopItemSO[btnNo].speed);
                shovelController.ActivateChildGameObjectByName(shopItemSO[btnNo].name);
            }
        }


    }
    public void LoadPanels()
    {
        for (int i = 0; i < 9; i++)
        {
            shopPanels[i].titleText.text = shopItemSO[i].title;
            shopPanels[i].descriptionText.text = shopItemSO[i].description;
            shopPanels[i].costText.text = "Basalts: " + shopItemSO[i].baseCost.ToString();
        }
    }
}
