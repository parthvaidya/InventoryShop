using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel 
{
    //set the required items 
    public float MaxWeight { get; private set; }
    public float CurrentWeight { get; private set; }
    private List<ShopItem> items;

    public event Action OnInventoryUpdated; //action for observer pattern
    public int ItemCount => items.Count;

    //create the inventory
    public InventoryModel(float maxWeight)
    {
        MaxWeight = maxWeight;
        CurrentWeight = 0f;
        items = new List<ShopItem>();
    }

    //Can add items
    public bool CanAddItem(ShopItem item, int quantity = 1)
    {
        return (CurrentWeight + item.weight * quantity) <= MaxWeight;
    }

    //Add the Items 
    public void AddItem(ShopItem item, int quantity = 1)
    {
        ShopItem existingItem = items.Find(i => i.itemName == item.itemName);
        if (existingItem != null) //check for existing item to update the quantity
        {
            existingItem.quantity += quantity;
        } else  {
            //add new item
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
        CurrentWeight += item.weight * quantity; //update the weight
        OnInventoryUpdated?.Invoke(); //invoke the inventory
    }

    //remove items after selling
    public void RemoveItem(ShopItem item, int quantity)
    {
        ShopItem existingItem = items.Find(i => i.itemName == item.itemName);
        if (existingItem != null)
        {
            int actualQuantityToRemove = Mathf.Min(existingItem.quantity, quantity); //keep positive values
            existingItem.quantity -= actualQuantityToRemove;
            CurrentWeight = Mathf.Max(0, CurrentWeight - item.weight * actualQuantityToRemove);
            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem); //remove items
            }
            OnInventoryUpdated?.Invoke(); //invoke to update
        }
    }

    //List of shop items
    public List<ShopItem> GetAllItems()
    {
        return new List<ShopItem>(items);
    }

    //Notify the Inventory that its updated
    public void NotifyInventoryUpdated()
    {
        OnInventoryUpdated?.Invoke();
    }

    //Reset the game
    public void ResetGame()
    {
        items.Clear();
        CurrentWeight = 0f;
        OnInventoryUpdated?.Invoke();
    }
}
