using UnityEngine;

public class ToggleStore : MonoBehaviour
{
    private GameObject shop;
    public PlayerController playerController;

    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shop");

        if (shop == null)
        {
            Debug.LogError("No GameObject with tag 'Shop' found!");
        }
        else
        {
            shop.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleShop();
        }
    }

    private void ToggleShop()
    {
        if (shop != null)
        {
            shop.SetActive(!shop.activeSelf);

            if (shop.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                if(playerController != null)
                {
                    playerController.enabled = false;
                }
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                if(playerController != null)
                {
                    playerController.enabled = true;
                }
            }
        }
    }
}
