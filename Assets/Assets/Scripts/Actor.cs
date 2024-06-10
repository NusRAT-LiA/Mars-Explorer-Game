using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;
    private ShopManager shopManager;
    private PlayerControl playerControl;
    public PlayerController playerController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)&&currentHealth <= 0)
        {
            playerController.enabled = true;
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        currentHealth = maxHealth;
        shopManager = FindObjectOfType<ShopManager>();
        playerControl = FindObjectOfType<PlayerControl>();

        if (shopManager == null)
        {
            Debug.LogError("ShopManager not found in the scene!");
        }
        if (playerControl == null)
        {
            Debug.LogError("PlayerControl not found in the scene!");
        }
        }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        { 
            if (gameObject.CompareTag("Basalts"))
            {
                if (shopManager != null)
                {
                    shopManager.AddCoins();
                }
            }
            if (playerControl != null)
            {
                playerControl.StoreItemIntoInventory(gameObject);
            }

            ActivateChildGameObjectByName(SceneManager.GetActiveScene().name);

            // Death(); 
        }
    }

    void ActivateChildGameObjectByName(string name)
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.gameObject.name == name)
            {
                child.gameObject.SetActive(true);
                playerController.enabled = false;
                break;
            }
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
