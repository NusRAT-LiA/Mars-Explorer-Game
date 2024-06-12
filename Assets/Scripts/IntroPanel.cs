using UnityEngine;

public class IntroPanel : MonoBehaviour
{
    public float displayTime = 5f;
    public KeyCode skipKey = KeyCode.X; 

    private float timer = 0f;
    private bool canSkip = false;

    void Start()
    {
        gameObject.SetActive(true);
        timer = 0f;
        canSkip = skipKey != KeyCode.None;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= displayTime || (canSkip && Input.GetKeyDown(skipKey)))
        {
            gameObject.SetActive(false);
        }
    }
}
