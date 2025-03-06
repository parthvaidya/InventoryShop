using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour
{
    
    public static ShopPopup Instance; //create an instance

    //add necessary fields
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private GameObject warningPanel;

    [SerializeField] private GameObject itemBoughtPanel;  // New panel for item bought
    [SerializeField] private TextMeshProUGUI boughtText;
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
        //initialze the popups as inactive
        popupPanel.SetActive(false);
        warningPanel.SetActive(false);
        itemBoughtPanel.SetActive(false);

        //bind buttons to the listeners
        closeButton.onClick.AddListener(ClosePopup);

        addButton.onClick.AddListener(IncreaseQuantity);
        removeButton.onClick.AddListener(DecreaseQuantity);
        buyButton.onClick.AddListener(BuyItem);
    }


    //show popup
    public void ShowItemPopup(ShopItem item, InventoryController inventoryController, InventoryView inventoryView) // Update to accept ShopItem
    {

        //Debug.Log("ShowItemPopup called with: " + (inventoryController != null ? "valid controller" : "NULL controller"));
        this.inventoryController = inventoryController;
        this.inventoryView = inventoryView;
        currentItem = item;
        currentQuantity = 1;
        UpdatePopupUI();

        popupPanel.SetActive(true);
    }


    //update the popup UI
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

    //increase quantiyt
    public void IncreaseQuantity()
    {
        //update current quantity 
        if (currentQuantity < currentItem.quantity)
        {
            SoundManager.Instance.Play(Sounds.ClickItem);
            currentQuantity++;
            UpdatePopupUI();
        }
    }

    //decrease the quantity
    public void DecreaseQuantity()
    {
        if (currentQuantity > 1)
        {
            SoundManager.Instance.Play(Sounds.ClickItem);
            currentQuantity--;
            UpdatePopupUI();
        }
    }

    

    public void BuyItem()
    {
        //initiaze the cost , money and quanity
        int totalCost = currentItem.buyPrice * currentQuantity;
        int playerMoney = CurrencyManager.Instance.GetCurrency();
        int purchasedQuantity = currentQuantity;
        currentItem.quantity -= purchasedQuantity;

        if (playerMoney < totalCost)
        {
            //warning panel for money 
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
        //decrement quantity
        currentItem.quantity -= currentQuantity;
        // Add item to inventory
        inventoryController.AddItemToInventory(currentItem, currentQuantity);

        // Reset quantity and update UI
        inventoryController.RefreshInventoryUI();
        inventoryView.UpdateWeightUI(inventoryController.CurrentWeight, inventoryController.MaxWeight);
        // Refresh inventory controller
        inventoryController.InventoryModel.NotifyInventoryUpdated();
       
        UpdatePopupUI();
        Canvas.ForceUpdateCanvases(); //used because the canvas forcefully updates as UI becomes slow in updating (Not the best practice but had to use )
        currentQuantity = 1;

        

        StartCoroutine(ShowBoughtPanel("Item Bought!!")); //show panel after item is bought
        

        if (currentItem.quantity <= 0) //new
        {
            //SoundManager.Instance.Play(Sounds.MoneyAdded);
            ClosePopup();
        }

        SoundManager.Instance.Play(Sounds.MoneyAdded);
    }

    //coroutine for warning 
    private IEnumerator ShowWarningPanel()
    {
        warningPanel.SetActive(true);
        SoundManager.Instance.Play(Sounds.Warning);
        yield return new WaitForSeconds(2f);
        warningPanel.SetActive(false);
    }

    //coroutine for item bought
    private IEnumerator ShowBoughtPanel(string message)
    {
        boughtText.text = message;
        itemBoughtPanel.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        
        itemBoughtPanel.SetActive(false);
    }

    //close popup
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
        
        // Refresh inventory and weight UI
        inventoryController.RefreshInventoryUI();
        inventoryView.UpdateWeightUI(inventoryController.CurrentWeight, inventoryController.MaxWeight);
    }
}
