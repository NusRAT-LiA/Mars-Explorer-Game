using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController _characterController;

    public Inventory inventory;
    // public GameObject dropPoint;
    public HUD Hud;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        inventory.ItemUsed += Inventory_ItemUsed;
    }

    void Update(){
        if(mItemToPickup!=null && Input.GetKeyDown(KeyCode.H)){
            Debug.Log("JJ");
            inventory.AddItem(mItemToPickup);
            mItemToPickup.OnPickup();
            Hud.CloseMessagePanel();
        }
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e){
        IInventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        // goItem.transform.position = dropPoint.transform.position;
    }

    private IInventoryItem mItemToPickup = null;
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Hello");
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        
        if (item != null)
        {
            mItemToPickup = item;
            Hud.OpenMessagePanel("");
        }
    }

    private void OnTriggerExit(Collider other) {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        
        if (item != null)
        {
            Hud.CloseMessagePanel();
            mItemToPickup = null;
        }
    }
}
