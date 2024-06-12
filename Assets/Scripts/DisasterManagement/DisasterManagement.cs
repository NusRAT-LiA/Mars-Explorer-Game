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

                yield return new WaitForSeconds(5f); 

                disasterParticleSystem.Play();
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                // Check for player hiding status and reload scene if necessary
                PlayerHide playerHideScript = FindObjectOfType<PlayerHide>();
                if (playerHideScript != null && !playerHideScript.prodIsActive)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

            yield return new WaitForSeconds(interval);  
        }
    }
}
