using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour
{
    
    public static ShopPopup Instance;

    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText, itemDescriptionText, itemDetailsText;
    [SerializeField] private TextMeshProUGUI quantityText, totalPriceText,  playerMoneyText , shopQuantityText;
    [SerializeField] private Button addButton, removeButton, buyButton, closeButton;

    private InventoryController inventoryController;
    private InventoryView inventoryView;

    private ShopItem currentItem; // Update to use ShopItem
    private int currentQuantity = 1;

    private void Awake()
    {
        Instance = this;
        popupPanel.SetActive(false);

        closeButton.onClick.AddListener(ClosePopup);

        addButton.onClick.AddListener(IncreaseQuantity);
        removeButton.onClick.AddListener(DecreaseQuantity);
        buyButton.onClick.AddListener(BuyItem);
    }

    public void ShowItemPopup(ShopItem item, InventoryController inventoryController, InventoryView inventoryView) // Update to accept ShopItem
    {

        Debug.Log("ShowItemPopup called with: " + (inventoryController != null ? "valid controller" : "NULL controller"));
        this.inventoryController = inventoryController;
        this.inventoryView = inventoryView;
        currentItem = item;
        currentQuantity = 1;
        UpdatePopupUI();

        popupPanel.SetActive(true);
    }

    private void UpdatePopupUI()
    {
        itemIcon.sprite = currentItem.icon;
        itemNameText.text = $"Name: {currentItem.itemName}";
        itemDescriptionText.text = $"Description: {currentItem.description}";
        itemDetailsText.text = $"Type: {currentItem.itemType}\n\nRarity: {currentItem.rarity}\n\nWeight: {currentItem.weight}\n\nBuy Price: {currentItem.buyPrice}G\n\nSell Price: {currentItem.sellPrice}G";

        quantityText.text = $"Quantity: {currentItem.quantity}";
        totalPriceText.text = $"Total: {currentItem.buyPrice * currentQuantity}G";

        int playerMoney = CurrencyManager.Instance.GetCurrency();
        playerMoneyText.text = $"Money: {playerMoney}G";
        shopQuantityText.text = $"{currentQuantity}";
    }

    public void IncreaseQuantity()
    {
        if (currentQuantity < currentItem.quantity)
        {
            currentQuantity++;
            UpdatePopupUI();
        }
    }

    public void DecreaseQuantity()
    {
        if (currentQuantity > 1)
        {
            currentQuantity--;
            UpdatePopupUI();
        }
    }

    //public void BuyItem()
    //{
    //    int totalCost = currentItem.buyPrice * currentQuantity;
    //    Debug.Log($"Bought {currentQuantity}x {currentItem.itemName} for {totalCost}G");
    //    popupPanel.SetActive(false);
    //}

    public void BuyItem()
    {
        int totalCost = currentItem.buyPrice * currentQuantity;
        int playerMoney = CurrencyManager.Instance.GetCurrency();

        if (playerMoney < totalCost)
        {
            Debug.Log("Not enough money to buy this item!");
            return;
        }

        // Deduct the total cost
        CurrencyManager.Instance.SpendCurrency(totalCost);
        //UpdatePopupUI();
        if (inventoryController == null)
        {
            Debug.LogError("InventoryController is null!");
            return;
        }

        // Add item to inventory
        inventoryController.AddItemToInventory(currentItem, currentQuantity);

        Debug.Log($"Bought {currentQuantity}x {currentItem.itemName} for {totalCost}G");

        // Reset quantity and update UI
        currentQuantity = 1;
        UpdatePopupUI();

        // Refresh inventory and weight UI
        inventoryController.RefreshInventoryUI();
        inventoryView.UpdateWeightUI(inventoryController.CurrentWeight, inventoryController.MaxWeight);

        ClosePopup();
    }



    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
