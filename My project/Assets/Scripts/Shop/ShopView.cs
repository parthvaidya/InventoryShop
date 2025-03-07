using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    // Import game objects 
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private Transform allContainer, consumableContainer, materialContainer, treasureContainer, weaponsContainer;
    
    //Initialize the Data structires
    public Dictionary<ItemType, Transform> categoryContainers = new Dictionary<ItemType, Transform>();
    private List<GameObject> displayedItems = new List<GameObject>();

    private void Start()
    {
        categoryContainers[ItemType.All] = allContainer;
        categoryContainers[ItemType.Consumables] = consumableContainer;
        categoryContainers[ItemType.Materials] = materialContainer;
        categoryContainers[ItemType.Treasures] = treasureContainer;
        categoryContainers[ItemType.Weapons] = weaponsContainer;
    }

    //DIsplay Items
    public void DisplayItems(List<ShopItem> items, ItemType category)
    {
        if (!categoryContainers.ContainsKey(category) || categoryContainers[category] == null)
        {
            Debug.LogError($"ShopView: No container assigned for category {category}!");
            return;
        }
        ClearDisplayedItems();

        foreach (var item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, categoryContainers[category]);
            Image itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();
            TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            itemIcon.sprite = item.icon;
            quantityText.text = item.quantity.ToString();
            newItem.GetComponent<Button>().onClick.AddListener(() => ShowItemDetails(item));
            displayedItems.Add(newItem);
        }
    }

    //Remove the items
    private void ClearDisplayedItems()
    {
        foreach (var obj in displayedItems)
        {
            Destroy(obj);
        }
        displayedItems.Clear();
    }

    //show details
    public void ShowItemDetails(ShopItem item)
    {
        if (item == null || ShopPopup.Instance == null)
        {
            return;
        }
        ShopPopup.Instance.ShowItemPopup(item, inventoryController, inventoryView);
    }
}
