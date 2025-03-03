using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel 
{
    

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
    public bool CanAddItem(ShopItem item, int quantity = 1)
    {
        return (CurrentWeight + item.weight * quantity) <= MaxWeight;
    }

    //public bool CanAddItem(ShopItem item)
    //{
    //    return (CurrentWeight + item.weight) <= MaxWeight;
    //}

    //public void AddItem(ShopItem item)
    //{
    //    if (CanAddItem(item))
    //    {
    //        items.Add(item);
    //        CurrentWeight = Mathf.Max(0, CurrentWeight + item.weight);
    //        OnInventoryUpdated?.Invoke();
    //    }
    //}

    //public void RemoveItem(ShopItem item, int quantity)
    //{
    //    ShopItem existingItem = items.Find(i => i == item);
    //    if (existingItem != null)
    //    {
    //        int actualQuantityToRemove = Mathf.Min(existingItem.quantity, quantity); // Prevent negative values
    //        existingItem.quantity -= actualQuantityToRemove;
    //        //CurrentWeight -= item.weight * actualQuantityToRemove;
    //        CurrentWeight = Mathf.Max(0, CurrentWeight - item.weight * actualQuantityToRemove);
    //        if (existingItem.quantity <= 0)
    //        {
    //            items.Remove(existingItem);
    //        }

    //        OnInventoryUpdated?.Invoke(); // Notify UI to refresh
    //    }
    //}

    public void AddItem(ShopItem item, int quantity = 1)
    {
        ShopItem existingItem = items.Find(i => i.itemName == item.itemName);

        if (existingItem != null)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            ShopItem newItem = new ShopItem
            {
                itemName = item.itemName,
                description = item.description,
                itemType = item.itemType,
                rarity = item.rarity,
                quantity = quantity,
                weight = item.weight,
                buyPrice = item.buyPrice,
                sellPrice = item.sellPrice,
                icon = item.icon
            };

            items.Add(newItem);
        }

        CurrentWeight += item.weight * quantity;
        OnInventoryUpdated?.Invoke();
    }

    public void RemoveItem(ShopItem item, int quantity)
    {
        ShopItem existingItem = items.Find(i => i.itemName == item.itemName);

        if (existingItem != null)
        {
            int actualQuantityToRemove = Mathf.Min(existingItem.quantity, quantity);
            existingItem.quantity -= actualQuantityToRemove;
            CurrentWeight = Mathf.Max(0, CurrentWeight - item.weight * actualQuantityToRemove);

            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }

            OnInventoryUpdated?.Invoke();
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
