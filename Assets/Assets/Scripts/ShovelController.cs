using UnityEngine;

public class ShovelController : MonoBehaviour {
    public void ActivateChildGameObjectByName(string name)
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            Debug.Log(child.gameObject.name);
            if (child.gameObject.name == name)
            {
                child.gameObject.SetActive(true);
            }
            else if(child.gameObject.name != "Shovel")
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
