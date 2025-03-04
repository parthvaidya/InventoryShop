using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel 
{
    

    public List<ShopItem> items; //list of items

    //create a model
    public ShopModel()
    {
        items = ShopServiceLocator.Instance.GetAllItems();
    }

    //get items by their types 
    public List<ShopItem> GetItemsByType(ItemType type)
    {
        return type == ItemType.Consumables || type == ItemType.Materials || type == ItemType.Treasures || type == ItemType.Weapons
            ? ShopServiceLocator.Instance.GetItemsByCategory(type)
            : ShopServiceLocator.Instance.GetAllItems();
    }


    //reset the game
    public void ResetGame()
    {
        
        items.Clear(); // Clear the shop's current items
        items = ShopServiceLocator.Instance.GetAllItems(); // Repopulate shop items
    }
}
