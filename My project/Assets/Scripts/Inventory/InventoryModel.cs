using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel 
{
    //public float MaxWeight { get; private set; }
    //public float CurrentWeight { get; private set; }
    //private List<ShopItem> items;

    //public event Action OnInventoryUpdated;

    //public int ItemCount => items.Count; // Returns how many items are in the inventory

    //public InventoryModel(float maxWeight)
    //{
    //    MaxWeight = maxWeight;
    //    CurrentWeight = 0f;
    //    items = new List<ShopItem>();
    //}

    //public bool CanAddItem(ShopItem item)
    //{
    //    return CurrentWeight + item.weight <= MaxWeight;
    //}

    //public void AddItem(ShopItem item)
    //{
    //    if (CanAddItem(item))
    //    {
    //        items.Add(item);
    //        CurrentWeight += item.weight;
    //        OnInventoryUpdated?.Invoke();
    //    }
    //}

    //public void RemoveItem(ShopItem item, int quantity)
    //{
    //    ShopItem existingItem = items.Find(i => i == item);
    //    if (existingItem != null)
    //    {
    //        existingItem.quantity -= quantity;
    //        CurrentWeight -= item.weight * quantity;

    //        if (existingItem.quantity <= 0)
    //        {
    //            items.Remove(existingItem);
    //        }

    //        OnInventoryUpdated?.Invoke(); // Notify UI to refresh
    //    }
    //}

    //public List<ShopItem> GetAllItems()
    //{
    //    return new List<ShopItem>(items);
    //}

    public float MaxWeight { get; private set; }
    public float CurrentWeight { get; private set; }
    private List<ShopItem> items;

    public event Action OnInventoryUpdated;

    public int ItemCount => items.Count;

    public InventoryModel(float maxWeight)
    {
        MaxWeight = maxWeight;
        CurrentWeight = 0f;
        items = new List<ShopItem>();
    }

    public bool CanAddItem(ShopItem item)
    {
        return (CurrentWeight + item.weight) <= MaxWeight;
    }

    public void AddItem(ShopItem item)
    {
        if (CanAddItem(item))
        {
            items.Add(item);
            CurrentWeight = Mathf.Max(0, CurrentWeight + item.weight);
            OnInventoryUpdated?.Invoke();
        }
    }

    public void RemoveItem(ShopItem item, int quantity)
    {
        ShopItem existingItem = items.Find(i => i == item);
        if (existingItem != null)
        {
            int actualQuantityToRemove = Mathf.Min(existingItem.quantity, quantity); // Prevent negative values
            existingItem.quantity -= actualQuantityToRemove;
            CurrentWeight -= item.weight * actualQuantityToRemove;

            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }

            OnInventoryUpdated?.Invoke(); // Notify UI to refresh
        }
    }

    public List<ShopItem> GetAllItems()
    {
        return new List<ShopItem>(items);
    }

    public void ResetGame()
    {
        items.Clear();
        CurrentWeight = 0f;
        OnInventoryUpdated?.Invoke();
    }

}
