using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]     private InventoryView inventoryView;
    [SerializeField] private ShopItemCollection itemCollection;

    private InventoryModel inventoryModel;

    private void Awake()
    {
        inventoryModel = new InventoryModel(50f);
        inventoryView.Initialize(GatherResources, CloseInventoryPanel);
    }

    private void GatherResources()
    {
        if (itemCollection == null || itemCollection.items.Count == 0)
        {
            Debug.LogError("No items found in the ShopItemCollection!");
            return;
        }

        List<ShopItem> availableItems = new List<ShopItem>(itemCollection.items);

        if (inventoryModel.ItemCount == 0)
        {
            // First-time gathering, add up to 3 items
            int itemsToAdd = Mathf.Min(3, availableItems.Count);
            for (int i = 0; i < itemsToAdd; i++)
            {
                ShopItem randomItem = availableItems[Random.Range(0, availableItems.Count)];
                if (inventoryModel.CanAddItem(randomItem))
                {
                    inventoryModel.AddItem(randomItem);
                    inventoryView.AddItemToInventory(randomItem);
                    availableItems.Remove(randomItem);
                }
            }
        }
        else
        {
            // Keep gathering until capacity is reached
            while (availableItems.Count > 0)
            {
                ShopItem randomItem = availableItems[Random.Range(0, availableItems.Count)];

                if (inventoryModel.CanAddItem(randomItem))
                {
                    inventoryModel.AddItem(randomItem);
                    inventoryView.AddItemToInventory(randomItem);
                }
                else
                {
                    inventoryView.ShowCapacityReachedPanel();
                    break;
                }

                availableItems.Remove(randomItem);
            }
        }

        inventoryView.UpdateWeightUI(inventoryModel.CurrentWeight, inventoryModel.MaxWeight);
    }

    private void CloseInventoryPanel()
    {
        inventoryView.CloseInventoryPanel();
    }
}
