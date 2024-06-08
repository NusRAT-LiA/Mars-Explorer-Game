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

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<9; i++){
            shopPanelsGO[i].SetActive(true);
            coinUI.text = "Basalts: " + coins.ToString();
            LoadPanels();
            CheckPurchaseable();
        }
    }

    void Update()
    {
        
    }

    public void AddCoins(){
        coins++;
        coinUI.text = "Basalts: " + coins.ToString();
        CheckPurchaseable();
    }

    public void CheckPurchaseable(){
        for(int i=0; i<9; i++){
            if(coins>=shopItemSO[i].baseCost)
                myPurchaseButton[i].interactable = true;
            else
                myPurchaseButton[i].interactable=false;
        } 
    }

    public void PurchaseItem(int btnNo){
        if(coins>=shopItemSO[btnNo].baseCost){
            coins = coins - shopItemSO[btnNo].baseCost;
            coinUI.text = "Basalts: " + coins.ToString();
            CheckPurchaseable();
        }
    }
    public void LoadPanels(){
        for(int i=0; i<9; i++){
            shopPanels[i].titleText.text = shopItemSO[i].title;
            // shopPanels[i].itemText.sprite = shopItemSO[i].itemImage;
            shopPanels[i].descriptionText.text = shopItemSO[i].description;
            shopPanels[i].costText.text = "Basalts: "+ shopItemSO[i].baseCost.ToString();
        }
    }
}
