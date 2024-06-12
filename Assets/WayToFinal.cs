using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WayToFinal : MonoBehaviour
{
    private Inventory inventory;
    public TextMeshProUGUI messageText; 

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        
        if (messageText == null)
        {
            messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();
        }
        
        messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (inventory != null && inventory.GetItemCount() >= 6)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "You have collected 7 items. Proceeding to the final scene.";

            StartCoroutine(ProceedToFinalScene());
        }
    }

    private IEnumerator ProceedToFinalScene()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Final");
    }
}
