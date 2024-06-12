using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI instructionText;
    // public TextMeshProUGUI scoreText;  
    public Button[] optionButtons;
    public AudioClip rightAnswerAudio;
    public AudioClip wrongAnswerAudio;
    public AudioClip GenAudio;
    private AudioSource audioSource;
    public PlayerController playerController;

    private List<Question> questions;
    private int currentQuestionIndex = -1;
    private int playerScore;
    private Dictionary<KeyCode, int> keyToOptionIndex;
    private Dictionary<string, List<Question>> sceneQuestions;
    private List<string> sceneNames = new List<string> { "Level-1", "Level-2", "Level-3", "Level-4", "Level-5" };

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        

        // Retrieve the player's score from PlayerPrefs or set it to 0 if not found
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        UpdateScoreDisplay();  
        
        questionPanel.SetActive(false);

        keyToOptionIndex = new Dictionary<KeyCode, int>
        {
            { KeyCode.Alpha1, 0 },
            { KeyCode.Alpha2, 1 },
            { KeyCode.Alpha3, 2 },
            { KeyCode.Alpha4, 3 }
        };

        // Initialize the scene questions dictionary
        sceneQuestions = new Dictionary<string, List<Question>>
        {
            {
                "Level-1", new List<Question>
                {
                    new Question("I am born from the depths of Lava reservoirs. Yet, I am the silent singer of silicon-poor compositions", new string[] {"1. Basalt", "2. Basaltic Shergottites", "3. Lherzolitic Shergottites"}, 2),
                    new Question("In the cosmic dance of time, I emerge from ancient flames. For eons, I've journeyed through the void, bearing tales untold, Each crystal a witness to the touch of Liquid Crystals, bold", new string[] {"1. Basalt", "2. Basaltic Shergottites", "3. Lherzolitic Shergottites"}, 1)
                }
            },
            {
                "Level-2", new List<Question>
                {
                    new Question("Born from lava's swift freeze, with quartz and feldspar it gleams; scarce alkali, well-grained sheen, in Martian landscapes, it's a dream", new string[] {"1.Basalt", "2.Dacite", "3.Granitoids", "4.Nili Fossae"}, 1),
                    new Question("Through hydrothermal embrace, life's essence may gleam, within these silent depths, a preserved story, unseen", new string[] {"1.Basalt", "2.Dacite", "3.Granitoids", "4.Carbonated Rocks"}, 3),
                    new Question("A treasure trove they found, with carbonate minerals, their mysteries unwound", new string[] {"1.Basalt", "2.Dacite", "3.Granitoids", "4.Nili Fossae"}, 3),
                }
            },
            {
                "Level-3", new List<Question>
                {
                    new Question("In realms where waters once whispered tales, Beneath the ancient gaze of Martian skies, I lie, a testament to time's gentle hand, Where secrets of life may yet arise.", new string[] {"1.Andesite", "2.Sedimentary Rocks", "3.Carbonate Rocks", "4.Nakhlite"}, 2),
                    new Question("I form the bedrock, in layers, stories unfold. From ancient waters or windswept sands, they say, Within my embrace, life's whispers may sway.", new string[] {"1.Dacite","2.Sedimentary Rocks","3.Carbonate Rocks","4.Nakhlite"}, 1)
                }
            },
            {
                "Level-4", new List<Question>
                {
                    new Question("Aqueous whispers in silent stone, where Martian skies once did roam. Minerals aligned in secret tales, within this ancient Martian veil.", new string[] {"1.Jarosite", "2.Sulfates", "3.Carbonates", "4.Hematite"}, 0),
                    new Question("In rusted hues, I stand with my friends, a testament to time's unending drone.", new string[] {"1.Jarosite", "2.Sulfates", "3.Hematite Outcrops", "4.Carbonates"}, 2),
                    new Question("A wanderer from Earth's twilight. Ejected by force, from impact's might, A tale of cosmic collision, in the starry night.", new string[] {"1.Bounce Rock", "2.Shergottite", "3.Martian Meteorite", "4.Pyroxene"}, 0),
                }
            }
        };

        // Load questions for the current scene
        string sceneName = SceneManager.GetActiveScene().name;
        LoadQuestionsForScene(sceneName);
    }

    void LoadQuestionsForScene(string sceneName)
    {
        if (sceneQuestions.TryGetValue(sceneName, out questions))
        {
            currentQuestionIndex = -1;
        }
        else
        {
            Debug.LogError("No questions found for the current scene: " + sceneName);
            questions = new List<Question>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestionPanel();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            questionPanel.SetActive(false);
            playerController.enabled=true;
            audioSource.Stop();

        }

        if (questionPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ShowNextQuestion();
            }

            foreach (var entry in keyToOptionIndex)
            {
                if (Input.GetKeyDown(entry.Key))
                {
                    OnOptionSelected(entry.Value);
                    break;
                }
            }
        }
    }

    void ToggleQuestionPanel()
    {   
        playerController.enabled=false;
        questionPanel.SetActive(!questionPanel.activeSelf);

        if (questionPanel.activeSelf)
        {
            ShowNextQuestion();
            audioSource.PlayOneShot(GenAudio);
        }
    }

    void ShowNextQuestion()
    {
        if (questions.Count == 0)
        {              
            playerController.enabled=true;

            LevelComplete();
            return;
        }

        currentQuestionIndex = (currentQuestionIndex + 1) % questions.Count;
        DisplayQuestion(questions[currentQuestionIndex]);
    }

    void DisplayQuestion(Question question)
    {
        questionText.text = question.question;
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.options[i];
        }
        instructionText.text = "Press 1, 2, 3, or 4 to answer. Press T to move to the next question.";
    }

    void OnOptionSelected(int selectedIndex)
    {
        if (selectedIndex >= 0 && selectedIndex < optionButtons.Length)
        {
            if (selectedIndex == questions[currentQuestionIndex].correctOption)
            {
                playerScore += 5;
                // UpdateScoreDisplay();  
                PlayerPrefs.SetInt("PlayerScore", playerScore);
                audioSource.PlayOneShot(rightAnswerAudio);
                questions.RemoveAt(currentQuestionIndex);
                currentQuestionIndex = -1;
                ShowNextQuestion();
            }
            else
            {
                audioSource.PlayOneShot(wrongAnswerAudio);
                instructionText.text = "Wrong answer. Try again or press T to skip.";
            }
        }
    }

    void UpdateScoreDisplay()
    {
        // scoreText.text = playerScore.ToString();
    }

    void LevelComplete()
    {
        questionPanel.SetActive(false);
        PlayerPrefs.SetInt("PlayerScore", playerScore);

        string currentSceneName = SceneManager.GetActiveScene().name;

        int currentIndex = sceneNames.IndexOf(currentSceneName);

        if (currentIndex != -1 && currentIndex + 1 < sceneNames.Count)
        {
            string nextSceneName = sceneNames[currentIndex + 1];
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("No more levels. Game completed!");
        }
    }
}


[System.Serializable]
public class Question
{
    public string question;
    public string[] options;
    public int correctOption;

    public Question(string question, string[] options, int correctOption)
    {
        this.question = question;
        this.options = options;
        this.correctOption = correctOption;
    }
}
