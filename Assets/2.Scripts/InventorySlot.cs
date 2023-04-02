using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake() {
        Deselect();
    }

    public void Select(){
        image.color = selectedColor;
    }

    public void Deselect(){
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0){
            GameObject dropped = eventData.pointerDrag;
            InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
        else{
            //Getting InventoryItem GameObject that is in the target InventorySlot.
            GameObject targetInventoryItem = transform.GetChild(0).gameObject;
            //print(targetInventoryItem);
            InventoryItem itemToSwap = targetInventoryItem.GetComponent<InventoryItem>();

            // Dropped InventoryItem GameObject
            GameObject dropped = eventData.pointerDrag;
            // print(dropped);
            InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();

            // Items Swap
            itemToSwap.transform.SetParent(inventoryItem.parentAfterDrag);
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
