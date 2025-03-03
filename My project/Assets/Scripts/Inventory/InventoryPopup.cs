using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    public static InventoryPopup Instance;

    [Header("UI References")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText, itemDescriptionText, itemDetailsText;
    [SerializeField] private TextMeshProUGUI quantityText, totalPriceText;
    [SerializeField] private TextMeshProUGUI inventoryQuantityText, expectedEarningsText;
    [SerializeField] private Button addButton, removeButton, sellButton, closeButton;

    private InventoryController inventoryController;
    private InventoryView inventoryView;
    private InventoryModel inventoryModel;
    private ShopItem currentItem;
    private int currentQuantity;

    private void Awake()
    {
        Instance = this;
        popupPanel.SetActive(false);

        closeButton.onClick.AddListener(ClosePopup);
        addButton.onClick.AddListener(IncreaseQuantity);
        removeButton.onClick.AddListener(DecreaseQuantity);
        //sellButton.onClick.AddListener(SellItem);

        sellButton.onClick.AddListener(OpenSellConfirmation);
    }

    public void ShowItemPopup(ShopItem item, InventoryController inventoryController, InventoryView inventoryView)
    {
        this.inventoryController = inventoryController;
        this.inventoryView = inventoryView;
        currentItem = item;
        currentQuantity = 1; // Start at 1 instead of full quantity
        UpdatePopupUI();

        popupPanel.SetActive(true);
    }

    private void UpdatePopupUI()
    {
        itemIcon.sprite = currentItem.icon;
        itemNameText.text = $"Name: {currentItem.itemName}";
        itemDescriptionText.text = $"Description: {currentItem.description}";
        itemDetailsText.text = $"Type: {currentItem.itemType}\n\nRarity: {currentItem.rarity}\n\nWeight: {currentItem.weight}\n\nSell Price: {currentItem.sellPrice}G";

        quantityText.text = $"Quantity: {currentItem.quantity}"; // Show total owned
        inventoryQuantityText.text = $"{currentQuantity}"; // Show how many are being sold
        expectedEarningsText.text = $"Earnings: {currentItem.sellPrice * currentQuantity}G"; // Update expected earnings
        totalPriceText.text = $"Total: {currentItem.sellPrice * currentQuantity}G"; // Update total price
    }

    public void IncreaseQuantity()
    {
        if (currentQuantity < currentItem.quantity) // Cannot exceed available quantity
        {
            currentQuantity++;
            UpdatePopupUI();
        }
    }

    public void DecreaseQuantity()
    {
        if (currentQuantity > 1) // Prevent going below 1
        {
            currentQuantity--;
            UpdatePopupUI();
        }
    }



    private void OpenSellConfirmation()
    {
        if (ConfirmationPopup.Instance == null)
        {
            Debug.LogError("ConfirmationPopup Instance is null! Ensure the popup exists in the scene.");
            return;
        }

        ConfirmationPopup.Instance.ShowConfirmation(
            $"Do you want to sell {currentQuantity}x {currentItem.itemName} for {currentItem.sellPrice * currentQuantity}G?",
            ConfirmSell
        );
    }

    private void ConfirmSell()
    {
        if (currentItem != null && currentQuantity > 0)
        {
            int totalSellPrice = currentItem.sellPrice * currentQuantity;
            CurrencyManager.Instance.AddCurrency(totalSellPrice);

            if (inventoryController == null)
            {
                Debug.LogError("InventoryController is null!");
                return;
            }

            //currentItem.quantity -= currentQuantity;
            //if (currentItem.quantity <= 0)
            //{
            //    inventoryController.RemoveItemFromInventory(currentItem, currentQuantity);
            //    ClosePopup(); // Close the popup if the item is gone
            //}
            //else
            //{
            //    UpdatePopupUI(); // Update the popup if some quantity remains
            //}
            if (currentItem.quantity > 0)
            {
                inventoryController.RemoveItemFromInventory(currentItem, currentQuantity);
            }
            else
            {
                ClosePopup(); // Close the popup if the item is gone
            }

            currentQuantity = 1;
            Debug.Log($"Attempting to sell {currentQuantity}x {currentItem?.itemName ?? "NULL"}");


            Debug.Log($"InventoryController is {inventoryController.CurrentWeight} , {inventoryController.MaxWeight}");
            inventoryController.RefreshInventoryUI();

            inventoryView.UpdateWeightUI(inventoryController.CurrentWeight, inventoryController.MaxWeight);



            UpdatePopupUI();
        }
    }
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
        inventoryController.RefreshInventoryUI();

        inventoryView.UpdateWeightUI(inventoryController.CurrentWeight, inventoryController.MaxWeight);
    }



}




