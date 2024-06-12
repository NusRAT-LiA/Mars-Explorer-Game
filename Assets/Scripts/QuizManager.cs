using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Runtime.InteropServices;

public class QuizManager : MonoBehaviour
{
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI instructionText;
    public Button[] optionButtons;
    public int correctOption;
    private int playerScore;
    // public TextMeshProUGUI score;


    private Dictionary<KeyCode, int> keyToOptionIndex;

    void Start()
    {

        keyToOptionIndex = new Dictionary<KeyCode, int>
        {
            { KeyCode.Alpha1, 0 },
            { KeyCode.Alpha2, 1 },
            { KeyCode.Alpha3, 2 },
            { KeyCode.Alpha4, 3 }
        };

    }


    void Update()
    {
        instructionText.text = "Press 1, 2, 3, or 4 to answer.";

        foreach (var entry in keyToOptionIndex)
            {
                if (Input.GetKeyDown(entry.Key))
                {
                    OnOptionSelected(entry.Value);
                    break;
                }
            }
    }


   

    void OnOptionSelected(int selectedIndex)
    {
        if (selectedIndex >= 0 && selectedIndex < optionButtons.Length)
        {
            if (selectedIndex == correctOption)
            {   
                playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
                playerScore+=2;
                PlayerPrefs.SetInt("PlayerScore", playerScore);
                // score.text=playerScore.ToString();
                instructionText.text = "Right answer.";
                questionPanel.SetActive(false);



            }
            else
            {
                instructionText.text = "Wrong answer.";
            }
        }
    }

    
}


