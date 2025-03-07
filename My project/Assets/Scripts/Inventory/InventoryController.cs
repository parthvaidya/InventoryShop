using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    //Add necessary gameobjects
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private ShopItemCollection itemCollection;

    private InventoryModel inventoryModel;
    public InventoryModel InventoryModel => inventoryModel;
    public float CurrentWeight => inventoryModel.CurrentWeight; 
    public float MaxWeight => inventoryModel.MaxWeight;
   
    //On awake pass the inventory capacity to be max 200
    private void Awake()
    {
        inventoryModel = new InventoryModel(200f);
        inventoryView.Initialize(GatherResources, CloseInventoryPanel);

        // Subscribe to model updates
        inventoryModel.OnInventoryUpdated += UpdateView;
    }

    private void GatherResources()
    {
        SoundHelper.PlaySound(Sounds.ShopItems);
        if (itemCollection == null || itemCollection.items.Count == 0) 
            return;

        List<ShopItem> availableItems = new List<ShopItem>(itemCollection.items);

        while (availableItems.Count > 0)
        {
            ShopItem randomItem = availableItems[UnityEngine.Random.Range(0, availableItems.Count)];

            if (inventoryModel.CanAddItem(randomItem))
            {
                inventoryModel.AddItem(randomItem);
            } else {
                inventoryView.ShowCapacityReachedPanel();
                break;
            }
            availableItems.Remove(randomItem);
        }
    }

    public void AddItemToInventory(ShopItem item, int quantity)
    {
        if (item == null) 
            return;

        if (inventoryModel.CanAddItem(item, quantity))
        {
            inventoryModel.AddItem(item, quantity);
        } else {
        
            inventoryView.ShowWarningPanel();
        }
    }

    public void RemoveItemFromInventory(ShopItem item, int quantity)
    {
        inventoryModel.RemoveItem(item, quantity);
    }
    public void ResetGame()
    {
        inventoryModel.ResetGame();
    }

    private void UpdateView()
    {
        inventoryView.RefreshInventoryUI(inventoryModel.GetAllItems());
        inventoryView.UpdateWeightUI(inventoryModel.CurrentWeight, inventoryModel.MaxWeight);
    }

    private void CloseInventoryPanel()
    {
        inventoryView.CloseInventoryPanel();
    }
}
