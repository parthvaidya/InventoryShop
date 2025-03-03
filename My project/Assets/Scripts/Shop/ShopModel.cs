using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel 
{
    

    public List<ShopItem> items;

    public ShopModel()
    {
        items = ShopServiceLocator.Instance.GetAllItems();
    }

    public List<ShopItem> GetItemsByType(ItemType type)
    {
        return type == ItemType.Consumables || type == ItemType.Materials || type == ItemType.Treasures || type == ItemType.Weapons
            ? ShopServiceLocator.Instance.GetItemsByCategory(type)
            : ShopServiceLocator.Instance.GetAllItems();
    }


    public void ResetGame()
    {
        Debug.Log("Resetting shop items...");
        items.Clear(); // Clear the shop's current items
        items = ShopServiceLocator.Instance.GetAllItems(); // Repopulate shop items
    }
}
