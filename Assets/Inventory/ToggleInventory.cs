using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    private GameObject inventory
    ;
    public PlayerController playerController;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.SetActive(true);

        if (inventory == null)
        {
            Debug.LogError("No GameObject with tag 'Inventory' found!");
        }
        else
        {
            inventory.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void Awake(){
        // inventory = GameObject.FindGameObjectWithTag("Inventory");

        // if (inventory == null)
        // {
        //     Debug.LogError("No GameObject with tag 'Inventory' found!");
        // }
        // else
        // {
        //     inventory.SetActive(false);
        //     Cursor.visible = false;
        //     Cursor.lockState = CursorLockMode.Locked;
        // }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        if (inventory != null)
        {
            Debug.Log("Kire");
            inventory.SetActive(!inventory.activeSelf);

            if (inventory.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                if (playerController != null)
                {
                    playerController.enabled = false;
                }
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                if (playerController != null)
                {
                    playerController.enabled = true;
                }
            }
        }
    }
}
