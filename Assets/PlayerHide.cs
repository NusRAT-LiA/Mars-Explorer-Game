using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public GameObject prodPrefab; // Reference to the prod prefab
    private GameObject currentProd; // Reference to the current prod instance
    public bool prodIsActive = false;

    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        // Check if the U key is pressed
        if (Input.GetKeyDown(KeyCode.U))
        {
            // Toggle the pod
            TogglePod();
        }
    }

    // Method to toggle the pod
    void TogglePod()
    {
        if (prodIsActive)
        {
            // Close the pod
            ClosePod();
            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
        else
        {
            // Open the pod
            OpenPod();
            if (playerController != null)
            {
                playerController.enabled = false;
            }
        }
    }

    // Method to open the pod
    void OpenPod()
    {
        if (currentProd == null)
        {
            // Spawn the pod GameObject at the player's position and rotation
            currentProd = Instantiate(prodPrefab, transform.position, transform.rotation);
            prodIsActive = true; // Set prod to active
        }
    }

    // Method to close the pod
    void ClosePod()
    {
        if (currentProd != null)
        {
            // Destroy the current pod GameObject
            Destroy(currentProd);
            prodIsActive = false; // Set prod to inactive
        }
    }
}
