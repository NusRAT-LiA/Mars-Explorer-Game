using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;
    private ShopManager shopManager;

    void Awake()
    {
        currentHealth = maxHealth;
        shopManager = FindObjectOfType<ShopManager>();

        if (shopManager == null)
        {
            Debug.LogError("ShopManager not found in the scene!");
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

            Death(); 
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
