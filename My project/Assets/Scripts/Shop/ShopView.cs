using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    

   [SerializeField] private GameObject itemPrefab; // Assign the prefab in the Inspector
    private TextMeshProUGUI itemDetailsText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InventoryView inventoryView;

    public Dictionary<ItemType, Transform> categoryContainers = new Dictionary<ItemType, Transform>();

    private List<GameObject> displayedItems = new List<GameObject>();

    [SerializeField] private Transform allContainer;
    [SerializeField] private Transform consumableContainer;
    [SerializeField] private Transform materialContainer;
    [SerializeField] private Transform treasureContainer;
    [SerializeField] private Transform weaponsContainer;

    private void Start()
    {
        categoryContainers[ItemType.All] = allContainer;
        categoryContainers[ItemType.Consumables] = consumableContainer;
        categoryContainers[ItemType.Materials] = materialContainer;
        categoryContainers[ItemType.Treasures] = treasureContainer;
        categoryContainers[ItemType.Weapons] = weaponsContainer;
    }

    public void DisplayItems(List<ShopItem> items, ItemType category)
    {
        if (!categoryContainers.ContainsKey(category) || categoryContainers[category] == null)
        {
            Debug.LogError($"ShopView: No container assigned for category {category}!");
            return;
        }

        Transform itemContainer = categoryContainers[category];

        // Clear previous items
        foreach (var obj in displayedItems)
        {
            Destroy(obj);
        }
        displayedItems.Clear();

        // Populate new items dynamically
        foreach (var item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, itemContainer);

            Image itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();
            TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            Image buttonBackground = newItem.GetComponent<Image>();

            itemIcon.sprite = item.icon;
            quantityText.text = item.quantity.ToString();

            buttonBackground.color = Color.white;

            newItem.GetComponent<Button>().onClick.AddListener(() => ShowItemDetails(item));

            displayedItems.Add(newItem);
            Debug.Log($"Item: {item.itemName}, Icon: {item.icon}, Quantity: {item.quantity}");
        }
    }

    public void ShowItemDetails(ShopItem item)
    {
        if (item == null)
        {
            Debug.LogError("ShowItemDetails: item is null!");
            return;
        }
        if (ShopPopup.Instance == null)
        {
            Debug.LogError("ShopPopup.Instance is null! Make sure the popup is in the scene and active.");
            return;
        }

        ShopPopup.Instance.ShowItemPopup(item, inventoryController, inventoryView);
    }
}
