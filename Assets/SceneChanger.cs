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
        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeToLevel2();
        }

    }

    void ChangeToLevel1()
    {
        SceneManager.LoadScene("Level-1");
    }
    void ChangeToLevel2()
    {
        SceneManager.LoadScene("Level-2");
    }
}
