using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private InventoryManager inventoryManager;

    private void Start() {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();;
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Player")){
            inventoryManager.AddItem(itemData);
        }
    }
}
