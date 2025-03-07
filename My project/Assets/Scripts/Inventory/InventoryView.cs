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
    [SerializeField] private InventoryController inventoryController;
    private List<GameObject> displayedItems = new List<GameObject>(); //display the items
    
    //initialze the buttons
    public void Initialize(System.Action onGatherResources, System.Action onClose)
    {
        gatherResourcesButton.onClick.RemoveAllListeners();
        gatherResourcesButton.onClick.AddListener(() => onGatherResources?.Invoke());
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => onClose?.Invoke());
        capacityReachedPanel.SetActive(false);
        warningPanel.SetActive(false);
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
        SoundHelper.PlaySound(Sounds.Warning);
        StartCoroutine(HideCapacityPanel());
    }

    //create coroutine for it
    private IEnumerator HideCapacityPanel()
    {
        yield return new WaitForSeconds(5f);
        capacityReachedPanel.SetActive(false);
    }

    public void RefreshInventoryUI(List<ShopItem> items)
    {
        foreach (var displayedItem in displayedItems)
        {
            Destroy(displayedItem);
        }
        displayedItems.Clear();

        foreach (var item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, inventoryContainer);
            Image itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();
            TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            itemIcon.sprite = item.icon;
            quantityText.text = item.quantity.ToString();
            newItem.GetComponent<Button>().onClick.AddListener(() => InventoryPopup.Instance.ShowItemPopup(item , inventoryController, this));
            displayedItems.Add(newItem);
        }
    }

    //close inventory panel
    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }
    
    //Warning panel
    public void ShowWarningPanel()
    {
        warningPanel.SetActive(true);
        SoundHelper.PlaySound(Sounds.PopupMusic);
        StartCoroutine(HideWarningPanel());
    }
    //Coroutine to hide it
    private IEnumerator HideWarningPanel()
    {
        yield return new WaitForSeconds(2f);
        warningPanel.SetActive(false);
    }
}
