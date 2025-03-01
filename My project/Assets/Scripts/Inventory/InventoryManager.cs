using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI References")]
    public Button gatherResourcesButton; // Assign in Inspector
    public GameObject itemPrefab; // Assign the inventory item prefab in the Inspector
    public Transform inventoryContainer; // Assign Inventory UI container
    public TextMeshProUGUI weightText; // Assign inventory weight UI

    [Header("Inventory Settings")]
    public float maxWeight = 50f; // Inventory max capacity

    [Header("Item Data")]
    public ShopItemCollection itemCollection; // Assign the ScriptableObject in Inspector

    private List<GameObject> displayedItems = new List<GameObject>();
    private float currentWeight = 0f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Attach GatherResources() directly in Awake
        if (gatherResourcesButton != null)
        {
            gatherResourcesButton.onClick.RemoveAllListeners(); // Ensure no duplicate listeners
            gatherResourcesButton.onClick.AddListener(GatherResources);
        }
        else
        {
            Debug.LogError("Gather Resources button is not assigned in Inspector!");
        }
    }

    public void GatherResources()
    {
        if (itemCollection == null || itemCollection.items.Count == 0)
        {
            Debug.LogError("No items found in the ShopItemCollection!");
            return;
        }

        List<ShopItem> availableItems = new List<ShopItem>(itemCollection.items);
        List<ShopItem> selectedItems = new List<ShopItem>();

        currentWeight = 0f;
        ClearInventory();

        while (selectedItems.Count < 4 && availableItems.Count > 0)
        {
            ShopItem randomItem = availableItems[Random.Range(0, availableItems.Count)];

            if (currentWeight + randomItem.weight <= maxWeight)
            {
                selectedItems.Add(randomItem);
                currentWeight += randomItem.weight;
            }
            availableItems.Remove(randomItem);
        }

        DisplayInventoryItems(selectedItems);
        UpdateWeightUI();
    }

    private void DisplayInventoryItems(List<ShopItem> items)
    {
        foreach (var item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, inventoryContainer);
            Image itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();
            TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();

            itemIcon.sprite = item.icon;
            quantityText.text = item.quantity.ToString();

            displayedItems.Add(newItem);
        }
    }

    private void ClearInventory()
    {
        foreach (var obj in displayedItems)
        {
            Destroy(obj);
        }
        displayedItems.Clear();
    }

    private void UpdateWeightUI()
    {
        weightText.text = $"{currentWeight} / {maxWeight} lbs";
    }
}
