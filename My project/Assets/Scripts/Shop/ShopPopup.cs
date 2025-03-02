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
    [SerializeField] private TextMeshProUGUI quantityText, totalPriceText;
    [SerializeField] private Button addButton, removeButton, buyButton, closeButton;

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

    public void ShowItemPopup(ShopItem item) // Update to accept ShopItem
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
        itemDetailsText.text = $"Type: {currentItem.itemType}\n\nRarity: {currentItem.rarity}\n\nWeight: {currentItem.weight}\n\nBuy Price: {currentItem.buyPrice}G\n\nSell Price: {currentItem.sellPrice}G";

        quantityText.text = $"Quantity: {currentItem.quantity}";
        totalPriceText.text = $"Total: {currentItem.buyPrice * currentQuantity}G";
    }

    public void IncreaseQuantity()
    {
        currentQuantity++;
        UpdatePopupUI();
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
        Debug.Log($"Bought {currentQuantity}x {currentItem.itemName} for {totalCost}G");
        popupPanel.SetActive(false);
    }

   

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
