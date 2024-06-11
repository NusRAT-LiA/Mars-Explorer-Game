using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class FinalQuiz : MonoBehaviour
{
    public TextMeshProUGUI questionText;      
    public Image questionImage;    
    public TextMeshProUGUI instructionText; 
    public TextMeshProUGUI scoreText; 
    public int score = 0; 
    private List<Quiz> questions = new List<Quiz>(); 
    public Sprite[] Sprites; 
    public Dictionary<Sprite, int> spriteToCorrectAnswerIndex; 
    private int currentQuestionIndex = 0;

    private Dictionary<KeyCode, int> keyToOptionIndex;

    public AudioClip correctAudio; // Audio clip for correct answer
    public AudioClip wrongAudio; // Audio clip for wrong answer
    private AudioSource audioSource; // AudioSource component for playing audio

    void Start()
    {
        keyToOptionIndex = new Dictionary<KeyCode, int>
        {
            { KeyCode.Alpha1, 0 },
            { KeyCode.Alpha2, 1 },
            { KeyCode.Alpha3, 2 },
            { KeyCode.Alpha4, 3 },
            { KeyCode.Alpha5, 4 }
        };

        spriteToCorrectAnswerIndex = new Dictionary<Sprite, int>
        {
            { Sprites[0], 2 }, 
            { Sprites[1], 0 }, 
            { Sprites[2], 3 },
            { Sprites[3], 3 },
            { Sprites[4], 4 },
            { Sprites[5], 4 },
            { Sprites[6], 3 },
            { Sprites[7], 0 },
            { Sprites[8], 2 },
            { Sprites[9], 2 },
        };

        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        GenerateQuestions();
        
        DisplayQuestion(currentQuestionIndex);
    }

    void Update()
    {
        foreach (var entry in keyToOptionIndex)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                OnOptionSelected(entry.Value);
                break;
            }
        }
    }

    void GenerateQuestions()
    {
        for (int i = 0; i < Sprites.Length; i++)
        {
            string question = $"Where did you see: {Sprites[i].name}?";
            int correctOptionIndex = spriteToCorrectAnswerIndex[Sprites[i]];

            questions.Add(new Quiz
            {
                questionText = question,
                questionSprite = Sprites[i],
                correctOptionIndex = correctOptionIndex
            });
        }
    }

    public void DisplayQuestion(int index)
    {
        if (index >= 0 && index < questions.Count)
        {
            questionText.text = questions[index].questionText;
            questionImage.sprite = questions[index].questionSprite;

            instructionText.text = "Press 1, 2, 3, 4 or 5 to answer.";
        }
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.Log("No more questions.");
        }
    }

    public void PreviousQuestion()
    {
        currentQuestionIndex--;
        if (currentQuestionIndex >= 0)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            currentQuestionIndex = 0; 
            Debug.Log("Already at the first question.");
        }
    }

    public void OnOptionSelected(int selectedIndex)
    {
        if (selectedIndex == questions[currentQuestionIndex].correctOptionIndex)
        {
            Debug.Log("Correct answer!");
            score++;
            scoreText.text = score.ToString();
            audioSource.PlayOneShot(correctAudio); // Play correct answer audio
        }
        else
        {
            Debug.Log("Wrong answer. Try again.");
            score--;
            scoreText.text = score.ToString();
            audioSource.PlayOneShot(wrongAudio); // Play wrong answer audio
        }
        NextQuestion();
    }
}

[System.Serializable]
public class Quiz
{
    public string questionText;
    public Sprite questionSprite;
    public int correctOptionIndex;
}
