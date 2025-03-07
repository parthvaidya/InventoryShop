using System;
using System.Collections.Generic;

public class ShopModel 
{
    //List of shop items
    public List<ShopItem> items;

    //event that observers subscribe to
    public event Action<List<ShopItem>> OnShopItemsUpdated; 

    //STore the items
    public ShopModel(List<ShopItem> initialItems)
    {
        items = new List<ShopItem>(initialItems);
    }

    //Get the item based on its types
    public List<ShopItem> GetItemsByType(ItemType type)
    {
        if (type == ItemType.All)
        {
            return new List<ShopItem>(items);
        }
        return items.FindAll(item => item.itemType == type);
    }

    // Notify observers when items are updated
    public void UpdateItems(List<ShopItem> newItems)
    {
        items = new List<ShopItem>(newItems);
        OnShopItemsUpdated?.Invoke(items); // Notify observers
    }


    //Reset the game
    public void ResetGame(List<ShopItem> initialItems)
    {
        items.Clear();
        items.AddRange(initialItems);
    }
}
