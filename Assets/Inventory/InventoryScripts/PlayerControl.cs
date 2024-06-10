using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Inventory inventory;
    // public GameObject dropPoint;
    public HUD Hud;


    void Start()
    {
        inventory.ItemUsed += Inventory_ItemUsed;
    }

    void Update(){
        if(mItemToPickup!=null){
            Debug.Log("JJ");
            inventory.AddItem(mItemToPickup);
            mItemToPickup = null;
            // mItemToPickup.OnPickup();
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
    public void StoreItemIntoInventory(GameObject rock) {
        Debug.Log("Hello");
        IInventoryItem item = rock.GetComponent<IInventoryItem>();
        
        if (item != null)
        {
            Debug.Log(item.Name);
            mItemToPickup = item;
            // Hud.OpenMessagePanel("");
        }
    }
}
