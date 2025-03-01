//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class InventoryManager : MonoBehaviour
//{
//    public static InventoryManager Instance;

//    [Header("UI References")]
//    [SerializeField] private Button gatherResourcesButton; // Assign in Inspector
//    [SerializeField] private GameObject itemPrefab; // Assign the inventory item prefab in the Inspector
//    [SerializeField] private Transform inventoryContainer; // Assign Inventory UI container
//    [SerializeField] private TextMeshProUGUI weightText; // Assign inventory weight UI
//    [SerializeField] private GameObject capacityReachedPanel; // UI panel to show when full
//    [SerializeField] private GameObject inventoryPanel;
//    [SerializeField] private Button closeButton;
//    [Header("Inventory Settings")]
//    private float maxWeight = 50f; // Inventory max capacity

//    [Header("Item Data")]
//    [SerializeField] private ShopItemCollection itemCollection; // Assign the ScriptableObject in Inspector

//    private List<GameObject> displayedItems = new List<GameObject>();
//    private float currentWeight = 0f;

//    private void Awake()
//    {
//        if (Instance == null) Instance = this;
//        else Destroy(gameObject);

//        if (gatherResourcesButton != null)
//        {
//            gatherResourcesButton.onClick.RemoveAllListeners();
//            gatherResourcesButton.onClick.AddListener(GatherResources);
//        }
//        else
//        {
//            Debug.LogError("Gather Resources button is not assigned in Inspector!");
//        }
//        if (closeButton != null)
//        {
//            closeButton.onClick.RemoveAllListeners();
//            closeButton.onClick.AddListener(CloseInventoryPanel);
//        }
//        else
//        {
//            Debug.LogError("Close button is not assigned in Inspector!");
//        }
//        if (capacityReachedPanel != null)
//        {
//            capacityReachedPanel.SetActive(false); // Hide initially
//        }
//    }

//    public void GatherResources()
//    {
//        if (itemCollection == null || itemCollection.items.Count == 0)
//        {
//            Debug.LogError("No items found in the ShopItemCollection!");
//            return;
//        }

//        List<ShopItem> availableItems = new List<ShopItem>(itemCollection.items);

//        if (displayedItems.Count == 0)
//        {
//            int itemsToAdd = Mathf.Min(3, availableItems.Count);
//            for (int i = 0; i < itemsToAdd; i++)
//            {
//                ShopItem randomItem = availableItems[Random.Range(0, availableItems.Count)];
//                if (currentWeight + randomItem.weight <= maxWeight)
//                {
//                    AddItemToInventory(randomItem);
//                    currentWeight += randomItem.weight;
//                    availableItems.Remove(randomItem);
//                }
//            }
//        }
//        else
//        {
//            while (availableItems.Count > 0)
//            {
//                ShopItem randomItem = availableItems[Random.Range(0, availableItems.Count)];

//                if (currentWeight + randomItem.weight <= maxWeight)
//                {
//                    AddItemToInventory(randomItem);
//                    currentWeight += randomItem.weight;
//                }
//                else
//                {
//                    ShowCapacityReachedPanel();
//                    break;
//                }

//                availableItems.Remove(randomItem);
//            }
//        }

//        UpdateWeightUI();
//    }

//    private void AddItemToInventory(ShopItem item)
//    {
//        GameObject newItem = Instantiate(itemPrefab, inventoryContainer);
//        Image itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();
//        TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();

//        itemIcon.sprite = item.icon;
//        quantityText.text = item.quantity.ToString();

//        displayedItems.Add(newItem);
//        newItem.GetComponent<Button>().onClick.AddListener(() => InventoryPopup.Instance.ShowItemPopup(item));
//    }

//    private void ShowCapacityReachedPanel()
//    {
//        if (capacityReachedPanel != null)
//        {
//            capacityReachedPanel.SetActive(true);
//            StopCoroutine(HideCapacityPanel());
//            StartCoroutine(HideCapacityPanel());
//        }
//    }

//    private IEnumerator HideCapacityPanel()
//    {
//        yield return new WaitForSeconds(5f);
//        capacityReachedPanel.SetActive(false);
//    }

//    private void UpdateWeightUI()
//    {
//        weightText.text = $"{currentWeight} / {maxWeight} KG";
//    }

//    public void CloseInventoryPanel()
//    {
//        if (inventoryPanel != null)
//        {
//            inventoryPanel.SetActive(false);
//        }
//    }
//}
