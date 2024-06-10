using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory inventory;
    public GameObject messagePanel;

    void Start()
    {
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        int index = -1;
        foreach(Transform slot in inventoryPanel){
            index++;

            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Transform textTransform = slot.GetChild(0).GetChild(1);
            RawImage rawImage = imageTransform.GetComponent<RawImage>();
            Text txtCount  = textTransform.GetComponent<Text>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if(index == e.Item.Slot.Id){
                rawImage.enabled = true;
                rawImage.texture = e.Item.Image.texture;

                int itemCount = e.Item.Slot.Count;

                if(itemCount>1){
                    txtCount.text = itemCount.ToString();
                }
                else{
                    txtCount.text = "";
                }

                itemDragHandler.Item = e.Item;
                break;
            }

        }
        // for (int i = 0; i < inventoryPanel.childCount; i++)
        // {
        //     Transform slot = inventoryPanel.GetChild(i);
        //     RawImage rawImage = slot.GetChild(0).GetChild(0).GetComponent<RawImage>();
        //     ItemDragHandler itemDragHandler = slot.GetChild(0).GetChild(0).GetComponent<ItemDragHandler>();

        //     if (!rawImage.enabled)
        //     {
        //         rawImage.enabled = true;
        //         rawImage.texture = e.Item.Image.texture;

        //         itemDragHandler.Item = e.Item;
        //         break;
        //     }
        // }
    }
    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory");

        int index = -1;
        foreach(Transform slot in inventoryPanel){
            index++;

            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Transform textTransform = slot.GetChild(0).GetChild(1);
            RawImage rawImage = imageTransform.GetComponent<RawImage>();
            Text txtCount  = textTransform.GetComponent<Text>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if(itemDragHandler.Item == null){
                continue;
            }

            if(e.Item.Slot.Id == index){
                int itemCount = e.Item.Slot.Count;
                itemDragHandler.Item = e.Item.Slot.FirstItem;

                if(itemCount<2){
                    txtCount.text = "";
                }
                else{
                    txtCount.text = itemCount.ToString();
                }

                if(itemCount==0){
                    rawImage.enabled = false;
                    rawImage.texture = null;
                }

            }

            if(index == e.Item.Slot.Id){
                rawImage.enabled = true;
                rawImage.texture = e.Item.Image.texture;

                int itemCount = e.Item.Slot.Count;

                if(itemCount>1){
                    txtCount.text = itemCount.ToString();
                }
                else{
                    txtCount.text = "";
                }

                itemDragHandler.Item = e.Item;
                break;
            }

        }


        // for (int i = 0; i < inventoryPanel.childCount; i++)
        // {
        //     Transform slot = inventoryPanel.GetChild(i);
        //     RawImage rawImage = slot.GetChild(0).GetChild(0).GetComponent<RawImage>();
        //     ItemDragHandler itemDragHandler = slot.GetChild(0).GetChild(0).GetComponent<ItemDragHandler>();

        //     if (itemDragHandler.Item.Equals(e.Item))
        //     {
        //         rawImage.enabled = false;
        //         rawImage.texture = null;

        //         itemDragHandler.Item = null;
        //         break;
        //     }
        // }
    }

    public void OpenMessagePanel(string text){
        messagePanel.SetActive(true);
    }

    public void CloseMessagePanel(){
        messagePanel.SetActive(false);
    }
}