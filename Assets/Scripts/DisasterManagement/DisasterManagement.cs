using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DisasterManagement : MonoBehaviour
{
    public GameObject disasterPrefab;  
    private ParticleSystem disasterParticleSystem;
    public AudioClip gameMusic; 
    private AudioSource audioSource;
    private float interval;
    public float upperRange;
    public float lowerRange;
    public TextMeshProUGUI warningText; 
    public TextMeshProUGUI gameOverText;  
    private const float disasterDuration = 10f;  // Duration for which the disaster plays

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (gameMusic != null)
        {
            audioSource.clip = gameMusic;
            audioSource.loop = true;
        }
        else
        {
            Debug.LogError("Game music not assigned.");
        }
    }

    void Start()
    {
        if (disasterPrefab != null)
        {
            disasterParticleSystem = disasterPrefab.GetComponent<ParticleSystem>();

            if (disasterParticleSystem != null)
            {
                StartCoroutine(ToggleParticleSystem());
            }
            else
            {
                Debug.LogError("No ParticleSystem component found on the disasterPrefab.");
            }
        }
        else
        {
            Debug.LogError("Disaster prefab not assigned.");
        }

        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);  
        }
        else
        {
            Debug.LogError("Warning Text UI element not assigned.");
        }

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);  
        }
        else
        {
            Debug.LogError("Game Over Text UI element not assigned.");
        }
    }

    IEnumerator ToggleParticleSystem()
    {
        while (true)
        {
            interval = Random.Range(lowerRange, upperRange);

            if (disasterParticleSystem.isPlaying)
            {
                disasterParticleSystem.Stop();
                audioSource.Stop();
                if (warningText != null)
                {
                    warningText.gameObject.SetActive(false);
                }
            }
            else
            {
                if (warningText != null)
                {
                    warningText.text = "Warning: Disaster imminent!";
                    warningText.gameObject.SetActive(true);
                }

                yield return new WaitForSeconds(5f);  // Wait for 5 seconds before starting the disaster

                disasterParticleSystem.Play();
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                
                PlayerHide playerHideScript = FindObjectOfType<PlayerHide>();
                if (playerHideScript != null && !playerHideScript.prodIsActive)
                {
                    StartCoroutine(GameOverRoutine());
                }

                yield return new WaitForSeconds(disasterDuration);  // Play disaster for 10 seconds

                disasterParticleSystem.Stop();
                audioSource.Stop();
                if (warningText != null)
                {
                    warningText.gameObject.SetActive(false);
                }
            }

            yield return new WaitForSeconds(interval);  // Adjust wait time to exclude the disaster and warning duration
        }
    }

    IEnumerator GameOverRoutine()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "Game Over";
            gameOverText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(5f);  // Wait for 5 seconds before reloading the scene

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
