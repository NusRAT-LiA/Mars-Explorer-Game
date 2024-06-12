using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    private int lastScore = -1; 

    void Start()
    {
        UpdateScoreDisplay();
    }

    void Update()
    {
        int currentScore = PlayerPrefs.GetInt("PlayerScore", 0);
        if (currentScore != lastScore)
        {
            lastScore = currentScore;
            UpdateScoreDisplay();
        }
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = lastScore.ToString();
    }
}
