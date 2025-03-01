using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    public static InventoryPopup Instance;

    [Header("UI References")]
    public GameObject popupPanel;
    public Image itemIcon;
    public TextMeshProUGUI itemNameText, itemDescriptionText, itemDetailsText;
    public TextMeshProUGUI quantityText, totalPriceText;
    public Button addButton, removeButton, sellButton, closeButton;

    private ShopItem currentItem;
    private int currentQuantity = 1;

    private void Awake()
    {
        Instance = this;
        popupPanel.SetActive(false);

        closeButton.onClick.AddListener(ClosePopup);
        addButton.onClick.AddListener(IncreaseQuantity);
        removeButton.onClick.AddListener(DecreaseQuantity);
        sellButton.onClick.AddListener(SellItem);
    }

    public void ShowItemPopup(ShopItem item)
    {
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
        itemDetailsText.text = $"Type: {currentItem.itemType}\n\nRarity: {currentItem.rarity}\n\nWeight: {currentItem.weight}\n\nSell Price: {currentItem.sellPrice}G";

        quantityText.text = $"Quantity: {currentItem.quantity}";
        totalPriceText.text = $"Total: {currentItem.sellPrice * currentQuantity}G";
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

    public void SellItem()
    {
        int totalSellPrice = currentItem.sellPrice * currentQuantity;
        Debug.Log($"Sold {currentQuantity}x {currentItem.itemName} for {totalSellPrice}G");
        popupPanel.SetActive(false);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
