using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item: ScriptableObject{

    [Header("Only gameplay")]
    public int id;
    public string itemName;
    public ItemType type;
    public int maxStackedItems = 1;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
}

public enum ItemType{
    Material,
    Consumable,
    Weapon,
    Armor
}