using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour
{
    
    public static ShopPopup Instance;

    [SerializeField] private GameObject popupPanel;
    [SerializeField] private GameObject warningPanel;
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
        warningPanel.SetActive(false);

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
        playerMoneyText.text = $"Coins: {playerMoney}";
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

    

    public void BuyItem()
    {
        int totalCost = currentItem.buyPrice * currentQuantity;
        int playerMoney = CurrencyManager.Instance.GetCurrency();
        int purchasedQuantity = currentQuantity;
        currentItem.quantity -= purchasedQuantity;

        if (playerMoney < totalCost)
        {
            StartCoroutine(ShowWarningPanel());
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
        currentItem.quantity -= currentQuantity;
        // Add item to inventory
        inventoryController.AddItemToInventory(currentItem, currentQuantity);

        //new

        // Reset quantity and update UI
        
       
        inventoryController.RefreshInventoryUI();
        inventoryView.UpdateWeightUI(inventoryController.CurrentWeight, inventoryController.MaxWeight);
        // Refresh inventory and weight UI
        inventoryController.InventoryModel.NotifyInventoryUpdated();
        UpdatePopupUI();
        Canvas.ForceUpdateCanvases();
        currentQuantity = 1;

        Debug.Log($"Added {currentQuantity}x {currentItem.itemName} to inventory.");
        Debug.Log($"Inventory now has {inventoryController.MaxWeight - inventoryController.CurrentWeight} KG available.");

        if (currentItem.quantity <= 0) //new
        {
            ClosePopup();
        }
    }

    private IEnumerator ShowWarningPanel()
    {
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningPanel.SetActive(false);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
        
        // Refresh inventory and weight UI
        inventoryController.RefreshInventoryUI();
        inventoryView.UpdateWeightUI(inventoryController.CurrentWeight, inventoryController.MaxWeight);
    }
}
