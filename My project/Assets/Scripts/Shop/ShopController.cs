using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    

    private ShopModel shopModel;
    public ShopView shopView;

    public Button allButton, consumablesButton, materialsButton, treasuresButton, weaponsButton;

    private void Start()
    {
        shopModel = new ShopModel();

        allButton.onClick.AddListener(() => UpdateShopView(ItemType.All));
        consumablesButton.onClick.AddListener(() => UpdateShopView(ItemType.Consumables));
        materialsButton.onClick.AddListener(() => UpdateShopView(ItemType.Materials));
        treasuresButton.onClick.AddListener(() => UpdateShopView(ItemType.Treasures));
        weaponsButton.onClick.AddListener(() => UpdateShopView(ItemType.Weapons));

        UpdateShopView(ItemType.Consumables);
    }

    public void UpdateShopView(ItemType itemType)
    {
        List<ShopItem> filteredItems = ShopServiceLocator.Instance.GetItemsByCategory(itemType);
        shopView.DisplayItems(filteredItems, itemType);
    }
}
