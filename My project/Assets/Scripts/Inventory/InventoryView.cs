using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button gatherResourcesButton;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private GameObject capacityReachedPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button closeButton;

    private List<GameObject> displayedItems = new List<GameObject>();

    public void Initialize(System.Action onGatherResources, System.Action onClose)
    {
        gatherResourcesButton.onClick.RemoveAllListeners();
        gatherResourcesButton.onClick.AddListener(() => onGatherResources?.Invoke());

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => onClose?.Invoke());

        capacityReachedPanel.SetActive(false);
    }

    public void UpdateWeightUI(float currentWeight, float maxWeight)
    {
        weightText.text = $"{currentWeight} / {maxWeight} KG";
    }

    public void ShowCapacityReachedPanel()
    {
        capacityReachedPanel.SetActive(true);
        StopCoroutine(HideCapacityPanel());
        StartCoroutine(HideCapacityPanel());
    }

    private IEnumerator HideCapacityPanel()
    {
        yield return new WaitForSeconds(5f);
        capacityReachedPanel.SetActive(false);
    }

    public void AddItemToInventory(ShopItem item, InventoryController inventoryController)
    {
        GameObject newItem = Instantiate(itemPrefab, inventoryContainer);
        Image itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();
        TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();

        itemIcon.sprite = item.icon;
        quantityText.text = item.quantity.ToString();

        displayedItems.Add(newItem);
        newItem.GetComponent<Button>().onClick.AddListener(() => InventoryPopup.Instance.ShowItemPopup(item, inventoryController, this));
    }

    public void RefreshInventoryUI(InventoryModel inventoryModel , InventoryController inventoryController)
    {
        // Clear previous UI
        foreach (var displayedItem in displayedItems)
        {
            Destroy(displayedItem);
        }
        displayedItems.Clear();

        // Re-add remaining items
        foreach (var item in inventoryModel.GetAllItems())
        {
            AddItemToInventory(item , inventoryController);
        }

        // Update weight UI
        UpdateWeightUI(inventoryModel.CurrentWeight, inventoryModel.MaxWeight);

        Debug.Log($"Refreshing inventory UI with {inventoryModel.ItemCount} items");
    }

    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }
}
