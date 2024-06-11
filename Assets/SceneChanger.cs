using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeToLevel1();
        }
    }

    void ChangeToLevel1()
    {
        Debug.Log("Changing to Level-1");
        SceneManager.LoadScene("Level-1");
    }
}
