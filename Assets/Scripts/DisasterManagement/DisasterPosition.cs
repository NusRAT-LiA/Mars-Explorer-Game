using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterPosition : MonoBehaviour
{
    public GameObject player;  // Reference to the player GameObject
    public float fixedY;       // Desired Y position

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Update the position of the disaster object to match the player's X and Z positions
            Vector3 newPosition = new Vector3(player.transform.position.x, fixedY, player.transform.position.z);
            transform.position = newPosition;
        }
    }
}
