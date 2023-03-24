using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public int maxStackedItems = 5;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject MainInventory;

    public Item[] itemsToPickup;

    int selectedSlot = -1;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        // Starts with the Toolbar's first item selected
        ChangeSelectedSlot(0);
    }

    private void Update() {
        // TODO: Refactor this. Use a more efficient method...
        // Checks if the input is a number and then changes the selected item
        if (Input.inputString != null){
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number <= 9){
                ChangeSelectedSlot(number -1);
            }
            if(isNumber && number == 0){
                ChangeSelectedSlot(9);
            }
        }

        //Open Inventory
        if(Input.GetKeyDown(KeyCode.B)){
            if(MainInventory.activeSelf == true){
                MainInventory.SetActive(false);
            }
            else{
                MainInventory.SetActive(true);
            } 
        }

        if(Input.GetKeyDown(KeyCode.O)){
            AddItem(itemsToPickup[0]);
        }

        if(Input.GetKeyDown(KeyCode.P)){
            AddItem(itemsToPickup[1]);
        }

    }

    private void ChangeSelectedSlot(int newValue){
        if(selectedSlot >= 0){
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item){
        // Check if any slot has the same item with count lower than max
        for (int i=0; i<inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if((itemInSlot != null) && (itemInSlot.item == item) && (itemInSlot.count <= maxStackedItems) && (itemInSlot.item.stackable == true)){
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Find any empty slot
        for (int i=0; i<inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                SpawnNewItem(item, slot);
                return true;
            }
        }
        
        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemObject.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use){
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null){
            Item item = itemInSlot.item;
            if (use == true){
                itemInSlot.count--;
                if(itemInSlot.count <= 0){
                    Destroy(itemInSlot.gameObject);
                }
                else{
                    itemInSlot.RefreshCount();
                }
            }
        }
        return null;
    }
}
