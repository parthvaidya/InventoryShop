using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel 
{
    public float MaxWeight { get; private set; }
    public float CurrentWeight { get; private set; }
    private List<ShopItem> items;

    public int ItemCount => items.Count; // Returns how many items are in the inventory

    public InventoryModel(float maxWeight)
    {
        MaxWeight = maxWeight;
        CurrentWeight = 0f;
        items = new List<ShopItem>();
    }

    public bool CanAddItem(ShopItem item)
    {
        return CurrentWeight + item.weight <= MaxWeight;
    }

    public void AddItem(ShopItem item)
    {
        if (CanAddItem(item))
        {
            items.Add(item);
            CurrentWeight += item.weight;
        }
    }
}
