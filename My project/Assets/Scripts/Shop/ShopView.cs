using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    public GameObject itemPrefab; // Assign the generic prefab in the Inspector
    private TextMeshProUGUI itemDetailsText;
    public Image itemIcon;

    // Dictionary to hold category-specific containers
    public Dictionary<ItemType, Transform> categoryContainers = new Dictionary<ItemType, Transform>();

    private List<GameObject> displayedItems = new List<GameObject>();

    // Assign containers in Unity Inspector (via GameObjects for each category)
    public Transform allContainer;
    public Transform consumableContainer;
    public Transform materialContainer;
    public Transform treasureContainer;
    public Transform weaponsContainer;

    private void Start()
    {
        // Initialize category containers
        categoryContainers[ItemType.All] = allContainer;
        categoryContainers[ItemType.Consumables] = consumableContainer;
        categoryContainers[ItemType.Materials] = materialContainer;
        categoryContainers[ItemType.Treasures] = treasureContainer;
        categoryContainers[ItemType.Weapons] = weaponsContainer;
    }

    //public void DisplayItems(List<ShopItems> items, ItemType category)
    //{
    //    if (!categoryContainers.ContainsKey(category) || categoryContainers[category] == null)
    //    {
    //        Debug.LogError($"ShopView: No container assigned for category {category}!");
    //        return;
    //    }

    //    Transform itemContainer = categoryContainers[category]; // Get correct container

    //    Debug.Log($"Displaying {items.Count} items in category {category}.");

    //    // Clear previous items
    //    foreach (var obj in displayedItems)
    //    {
    //        Destroy(obj);
    //    }
    //    displayedItems.Clear();

    //    // Populate new items dynamically
    //    foreach (var item in items)
    //    {
    //        GameObject newItem = Instantiate(itemPrefab, itemContainer);

    //        // Get components from the instantiated prefab directly
    //        Image itemImage = newItem.GetComponentInChildren<Image>();
    //        TextMeshProUGUI quantityText = newItem.GetComponentInChildren<TextMeshProUGUI>();

    //        // Assign data
    //        itemImage.sprite = item.icon;
    //        quantityText.text = item.quantity.ToString();

    //        // Attach click event to show details
    //        newItem.GetComponent<Button>().onClick.AddListener(() => ShowItemDetails(item));

    //        displayedItems.Add(newItem);
    //        Debug.Log($"Item: {item.itemName}, Icon: {item.icon}, Quantity: {item.quantity}");
    //    }
    //}

    public void DisplayItems(List<ShopItems> items, ItemType category)
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

            // Get the ItemIcon specifically
            Image itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();

            // Get the Quantity Text specifically
            TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();

            // Get the Button Background Image separately
            Image buttonBackground = newItem.GetComponent<Image>();

            // Assign data
            itemIcon.sprite = item.icon; // Correctly assigns the item sprite
            quantityText.text = item.quantity.ToString();

            // (Optional) Change button background color if needed
            buttonBackground.color = Color.white;  // You can customize this dynamically

            // Attach click event to show details
            newItem.GetComponent<Button>().onClick.AddListener(() => ShowItemDetails(item));

            displayedItems.Add(newItem);
            Debug.Log($"Item: {item.itemName}, Icon: {item.icon}, Quantity: {item.quantity}");
        }
    }

    

    public void ShowItemDetails(ShopItems item)
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

        // Only show the popup, skip text/icon logic
        ShopPopup.Instance.ShowItemPopup(item);
    }
}
