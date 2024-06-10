using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInventoryItem
{
    private Vector3 initialPosition; // Store the initial position when the item is spawned

    public virtual string Name
    {
        get { return "BaseItem"; }
    }

    public Sprite _Image = null;

    public Sprite Image
    {
        get { return _Image; }
    }

    public InventorySlot Slot
    {
        get; set;
    }


    public virtual void OnPickup()
    {
        // Reset the item's position to the initial position when it's picked up
        gameObject.SetActive(false);
    }

    public virtual void OnUse()
    {
    }

    public virtual void OnDrop()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Find the GameObject tagged "DropPosition"
            GameObject dropPosition = GameObject.FindWithTag("DropPosition");

            if (dropPosition != null)
            {
                // Set the item's position to the drop position
                rb.MovePosition(dropPosition.transform.position);
            }
            else
            {
                Debug.LogWarning("Drop position object not found with tag 'DropPosition'.");
            }
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found.");
        }
    }
}
