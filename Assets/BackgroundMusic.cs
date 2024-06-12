using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip gameMusic;  // Assign the audio clip in the Inspector
    private AudioSource audioSource;

    void Awake()
    {
        // Ensure this object persists across scenes
        DontDestroyOnLoad(gameObject);

        // Add an AudioSource component if one doesn't already exist
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip and configure the AudioSource
        audioSource.clip = gameMusic;
        audioSource.loop = true;  // Ensure the music loops
        audioSource.playOnAwake = true;  // Optionally, play on awake

        // Play the music
        audioSource.Play();
    }
}
