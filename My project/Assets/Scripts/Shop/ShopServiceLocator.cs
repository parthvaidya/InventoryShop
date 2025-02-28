using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopServiceLocator : MonoBehaviour
{
    //public static ShopServiceLocator Instance { get; private set; }

    //[SerializeField] private List<ShopItems> allItems; // Assign items in the Inspector

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public List<ShopItems> GetItemsByCategory(ItemType category)
    //{
    //    Debug.Log($"Fetching items for category: {category}");

    //    // If "All" is selected, return the entire list instead of filtering
    //    if (category == ItemType.All)
    //    {
    //        return new List<ShopItems>(allItems);
    //    }

    //    return allItems.FindAll(item => item.itemType == category);
    //}

    //public List<ShopItems> GetAllItems()
    //{
    //    return allItems;
    //}

    public static ShopServiceLocator Instance { get; private set; }

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

    private void LoadAllItems()
    {
        if (itemCollection != null)
        {
            allItems = new List<ShopItem>(itemCollection.items);
        }
    }

    public List<ShopItem> GetItemsByCategory(ItemType category)
    {
        Debug.Log($"Fetching items for category: {category}");

        if (category == ItemType.All)
        {
            return new List<ShopItem>(allItems);
        }

        return allItems.FindAll(item => item.itemType == category);
    }

    public List<ShopItem> GetAllItems()
    {
        return allItems;
    }
}
