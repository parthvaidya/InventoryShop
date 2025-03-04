using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    //Add necessary gameobjects
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private ShopItemCollection itemCollection;
    [SerializeField] private GameObject warningPanel;

    private InventoryModel inventoryModel;

    public InventoryModel InventoryModel => inventoryModel;

    public float CurrentWeight => inventoryModel.CurrentWeight; 
    public float MaxWeight => inventoryModel.MaxWeight;

    private void Awake()
    {
        //Set the inventory limit to 200
        inventoryModel = new InventoryModel(200f);
        inventoryView.Initialize(GatherResources, CloseInventoryPanel); //initialize it
        inventoryModel.OnInventoryUpdated += () => inventoryView.RefreshInventoryUI(inventoryModel , this);
        warningPanel.SetActive(false);
    }

    private void GatherResources()
    {
        //Initialize gather resources
        SoundManager.Instance.Play(Sounds.ShopItems);
        if (itemCollection == null || itemCollection.items.Count == 0) //check if items are 0
        {
            Debug.LogError("No items found in the ShopItemCollection!");
            return;
        }

        List<ShopItem> availableItems = new List<ShopItem>(itemCollection.items); //Available items

        if (inventoryModel.ItemCount == 0)
        {
            // First-time  add up to 3 items
            int itemsToAdd = Mathf.Min(3, availableItems.Count);
            for (int i = 0; i < itemsToAdd; i++)
            {
                ShopItem randomItem = availableItems[Random.Range(0, availableItems.Count)]; 
                if (inventoryModel.CanAddItem(randomItem)) 
                {
                    inventoryModel.AddItem(randomItem);
                    //inventoryView.AddItemToInventory(randomItem , this);
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
                    inventoryView.ShowCapacityReachedPanel(); //capacity reached
                    break;
                }

                availableItems.Remove(randomItem);
            }
        }

        inventoryView.UpdateWeightUI(inventoryModel.CurrentWeight, inventoryModel.MaxWeight); //update the weight at real time
    }

    //remove items from inventory
    public void RemoveItemFromInventory(ShopItem item, int quantity)
    {
        inventoryModel.RemoveItem(item, quantity);
        inventoryView.RefreshInventoryUI(inventoryModel , this); // Update UI
        inventoryView.UpdateWeightUI(inventoryModel.CurrentWeight, inventoryModel.MaxWeight); //update weight once removed
        //Debug.Log($"Updated InventoryController: {CurrentWeight} / {MaxWeight}");
    }

    public void RefreshInventoryUI()
    {
        inventoryView.RefreshInventoryUI(inventoryModel, this); //refresh the inventory
    }

    //reset the game
    public void ResetGame() 
    {
        inventoryModel.ResetGame();
        inventoryView.RefreshInventoryUI(inventoryModel, this);
    }

    public void AddItemToInventory(ShopItem item, int quantity)
    {
        if (item == null) return; //check for null

        if (inventoryModel.CanAddItem(item, quantity)) //check if item could be added
        {
            inventoryModel.AddItem(item, quantity);
            inventoryModel.NotifyInventoryUpdated();
        }
        else
        {
            StartCoroutine(ShowWarningPanel()); //warning for less space
            SoundManager.Instance.Play(Sounds.PopupMusic);
            
        }

        RefreshInventoryUI(); //referesh UI for updates
    }

    //couroutine for warning panel to be shown for 2 seconds
    private IEnumerator ShowWarningPanel()
    {
        warningPanel.SetActive(true);
        SoundManager.Instance.Play(Sounds.PopupMusic);
        yield return new WaitForSeconds(2f);
        warningPanel.SetActive(false);
    }
    private void CloseInventoryPanel()
    {
        inventoryView.CloseInventoryPanel(); //close the panel
    }
}
