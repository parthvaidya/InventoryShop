using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    ////connect the view and model
    //[SerializeField] private ShopModel shopModel;
    //[SerializeField] public ShopView shopView;

    //[SerializeField] private Button allButton, consumablesButton, materialsButton, treasuresButton, weaponsButton;

    //private void Start()
    //{
    //    shopModel = new ShopModel();

    //    //bind buttons at start
    //    allButton.onClick.AddListener(() => UpdateShopView(ItemType.All));
    //    consumablesButton.onClick.AddListener(() => UpdateShopView(ItemType.Consumables));
    //    materialsButton.onClick.AddListener(() => UpdateShopView(ItemType.Materials));
    //    treasuresButton.onClick.AddListener(() => UpdateShopView(ItemType.Treasures));
    //    weaponsButton.onClick.AddListener(() => UpdateShopView(ItemType.Weapons));

    //    UpdateShopView(ItemType.Consumables);
    //}

    ////update the shop view by displaying items
    //public void UpdateShopView(ItemType itemType)
    //{
    //    List<ShopItem> filteredItems = ShopServiceLocator.Instance.GetItemsByCategory(itemType);
    //    shopView.DisplayItems(filteredItems, itemType);
    //}

    ////Reset the game
    //public void ResetGame()
    //{
    //    shopModel.ResetGame();
    //    UpdateShopView(ItemType.All); // Refresh the UI
    //}


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
        BindUIButtons();
        UpdateShopView(ItemType.All);
    }

    private void BindUIButtons()
    {
        allButton.onClick.AddListener(() => UpdateShopView(ItemType.All));
        consumablesButton.onClick.AddListener(() => UpdateShopView(ItemType.Consumables));
        materialsButton.onClick.AddListener(() => UpdateShopView(ItemType.Materials));
        treasuresButton.onClick.AddListener(() => UpdateShopView(ItemType.Treasures));
        weaponsButton.onClick.AddListener(() => UpdateShopView(ItemType.Weapons));
    }

    public void UpdateShopView(ItemType itemType)
    {
        List<ShopItem> filteredItems = shopModel.GetItemsByType(itemType);
        shopView.DisplayItems(filteredItems, itemType);
    }

    public void ResetGame()
    {
        shopModel.ResetGame(itemCollection.items);
        UpdateShopView(ItemType.All);
    }
}
