using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    //add necessary game objects
    [Header("UI References")]
    [SerializeField] private Button gatherResourcesButton;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private GameObject capacityReachedPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject warningPanel;

    private List<GameObject> displayedItems = new List<GameObject>(); //display the items

    //initialze the buttons
    public void Initialize(System.Action onGatherResources, System.Action onClose)
    {
        gatherResourcesButton.onClick.RemoveAllListeners();
        gatherResourcesButton.onClick.AddListener(() => onGatherResources?.Invoke());
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => onClose?.Invoke());
        capacityReachedPanel.SetActive(false);
    }

    //update the weight 
    public void UpdateWeightUI(float currentWeight, float maxWeight)
    {
        weightText.text = $"{currentWeight} / {maxWeight} KG";
    }

    //show capacity reached
    public void ShowCapacityReachedPanel()
    {
        capacityReachedPanel.SetActive(true);
        SoundManager.Instance.Play(Sounds.Warning);
        StopCoroutine(HideCapacityPanel());
        StartCoroutine(HideCapacityPanel());
    }

    //create coroutine for it
    private IEnumerator HideCapacityPanel()
    {
        yield return new WaitForSeconds(5f);
        capacityReachedPanel.SetActive(false);
    }

    //add items to inventory
    public void AddItemToInventory(ShopItem item, InventoryController inventoryController)
    {
        //find the item icon and text
        GameObject newItem = Instantiate(itemPrefab, inventoryContainer);
        Image itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();
        TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        itemIcon.sprite = item.icon;
        quantityText.text = item.quantity.ToString();
        displayedItems.Add(newItem); //display items
        newItem.GetComponent<Button>().onClick.AddListener(() => InventoryPopup.Instance.ShowItemPopup(item, inventoryController, this));
    }

    //refresh inventory
    public void RefreshInventoryUI(InventoryModel inventoryModel , InventoryController inventoryController)
    {
        // Clear previous UI elements
        foreach (var displayedItem in displayedItems)
        {
            Destroy(displayedItem);
        }
        displayedItems.Clear();

        // Re-add the remaining items
        foreach (var item in inventoryModel.GetAllItems())
        {
            AddItemToInventory(item , inventoryController);
        }

        // Update weight UI
        UpdateWeightUI(inventoryModel.CurrentWeight, inventoryModel.MaxWeight);
    }


    //close inventory panel
    public void CloseInventoryPanel()
    {
        SoundManager.Instance.Play(Sounds.ClickItem);
        inventoryPanel.SetActive(false);
    }
    
    //Warning panel
    public void ShowWarningPanel()
    {
        warningPanel.SetActive(true);
        SoundManager.Instance.Play(Sounds.PopupMusic);
        StartCoroutine(HideWarningPanel());
    }
    //Coroutine to hide it
    private IEnumerator HideWarningPanel()
    {
        yield return new WaitForSeconds(2f);
        warningPanel.SetActive(false);
    }
}
