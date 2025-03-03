using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private ShopItemCollection itemCollection;

    private InventoryModel inventoryModel;

    public InventoryModel InventoryModel => inventoryModel;

    public float CurrentWeight => inventoryModel.CurrentWeight;
    public float MaxWeight => inventoryModel.MaxWeight;

    private void Awake()
    {
        inventoryModel = new InventoryModel(200f);
        inventoryView.Initialize(GatherResources, CloseInventoryPanel);
        inventoryModel.OnInventoryUpdated += () => inventoryView.RefreshInventoryUI(inventoryModel , this);
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

    public void RemoveItemFromInventory(ShopItem item, int quantity)
    {
        inventoryModel.RemoveItem(item, quantity);
        inventoryView.RefreshInventoryUI(inventoryModel , this); // Update UI
        inventoryView.UpdateWeightUI(inventoryModel.CurrentWeight, inventoryModel.MaxWeight);
        Debug.Log($"Updated InventoryController: {CurrentWeight} / {MaxWeight}");
    }

    public void RefreshInventoryUI()
    {
        inventoryView.RefreshInventoryUI(inventoryModel, this);
    }

    public void ResetGame()
    {
        inventoryModel.ResetGame();
        inventoryView.RefreshInventoryUI(inventoryModel, this);
    }

    public void AddItemToInventory(ShopItem item, int quantity)
    {
        if (item == null) return;

        if (inventoryModel.CanAddItem(item, quantity))
        {
            inventoryModel.AddItem(item, quantity);
            inventoryModel.NotifyInventoryUpdated();
        }
        else
        {
            Debug.LogWarning("Not enough inventory space!");
        }

        RefreshInventoryUI();
    }

    private void CloseInventoryPanel()
    {
        inventoryView.CloseInventoryPanel();
    }
}
