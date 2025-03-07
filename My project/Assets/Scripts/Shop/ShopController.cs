using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    //Initialzie
    private ShopModel shopModel;
    public ShopView shopView;

    [SerializeField] private Button allButton, consumablesButton, materialsButton, treasuresButton, weaponsButton;
    [SerializeField] private ShopItemCollection itemCollection; // The item collection asset

    private void Start()
    {
        if (itemCollection == null)
        {
            Debug.LogError("Item collection is not assigned!");
            return;
        }
        shopModel = new ShopModel(itemCollection.items);
        shopModel.OnShopItemsUpdated += UpdateShopView;
        
        BindUIButtons();
        UpdateShopView(shopModel.GetItemsByType(ItemType.All));
    }

   //Bind UI buttons
    private void BindUIButtons()
    {
        allButton.onClick.AddListener(() => shopModel.UpdateItems(shopModel.GetItemsByType(ItemType.All)));
        consumablesButton.onClick.AddListener(() => shopModel.UpdateItems(shopModel.GetItemsByType(ItemType.Consumables)));
        materialsButton.onClick.AddListener(() => shopModel.UpdateItems(shopModel.GetItemsByType(ItemType.Materials)));
        treasuresButton.onClick.AddListener(() => shopModel.UpdateItems(shopModel.GetItemsByType(ItemType.Treasures)));
        weaponsButton.onClick.AddListener(() => shopModel.UpdateItems(shopModel.GetItemsByType(ItemType.Weapons)));
    }

    //Update the view
    private void UpdateShopView(List<ShopItem> updatedItems)
    {
        shopView.DisplayItems(updatedItems, ItemType.All);
    }

    //Reset Game
    public void ResetGame()
    {
        shopModel.ResetGame(itemCollection.items);
    }
}
