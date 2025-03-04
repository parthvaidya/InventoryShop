using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopServiceLocator : MonoBehaviour
{
   

    public static ShopServiceLocator Instance { get; private set; } //create singleton

    [SerializeField] private ShopItemCollection itemCollection; // Single asset containing multiple items

    private List<ShopItem> allItems = new List<ShopItem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadAllItems();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //load all items
    private void LoadAllItems()
    {
        if (itemCollection != null)
        {
            allItems = new List<ShopItem>(itemCollection.items);
        }
    }

    //fetch items by category
    public List<ShopItem> GetItemsByCategory(ItemType category)
    {
        //Debug.Log($"Fetching items for category: {category}");

        if (category == ItemType.All)
        {
            return new List<ShopItem>(allItems);
        }

        return allItems.FindAll(item => item.itemType == category);
    }

    public List<ShopItem> GetAllItems()
    {
        return allItems; //return all items found in the asset
    }
}
