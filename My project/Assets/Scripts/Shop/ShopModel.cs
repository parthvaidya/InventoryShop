using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel 
{
    public List<ShopItems> items;

    public ShopModel()
    {
        items = ShopServiceLocator.Instance.GetAllItems();
    }

    public List<ShopItems> GetItemsByType(ItemType type)
    {
        return type == ItemType.Consumables || type == ItemType.Materials || type == ItemType.Treasures || type == ItemType.Weapons
            ? ShopServiceLocator.Instance.GetItemsByCategory(type)
            : ShopServiceLocator.Instance.GetAllItems();
    }
}
