using UnityEngine;

public class DestroyOnKeyPress : MonoBehaviour
{
    void Update()
    {
        // Check if the "X" key is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Destroy the attached GameObject
            gameObject.SetActive(false);
        }
    }
}
