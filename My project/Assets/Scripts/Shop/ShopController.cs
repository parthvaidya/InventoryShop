using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
   
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

    //Bind the Ui buttons to listeners
    private void BindUIButtons()
    {
        allButton.onClick.AddListener(() => UpdateShopView(ItemType.All));
        consumablesButton.onClick.AddListener(() => UpdateShopView(ItemType.Consumables));
        materialsButton.onClick.AddListener(() => UpdateShopView(ItemType.Materials));
        treasuresButton.onClick.AddListener(() => UpdateShopView(ItemType.Treasures));
        weaponsButton.onClick.AddListener(() => UpdateShopView(ItemType.Weapons));
    }

    //Update the shop view based on new items
    public void UpdateShopView(ItemType itemType)
    {
        List<ShopItem> filteredItems = shopModel.GetItemsByType(itemType);
        shopView.DisplayItems(filteredItems, itemType);
    }

    //Reset the game
    public void ResetGame()
    {
        shopModel.ResetGame(itemCollection.items);
        UpdateShopView(ItemType.All);
    }
}
