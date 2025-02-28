using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour
{
    public static ShopPopup Instance { get; private set; }

    public GameObject popupPanel;
    public Image popupIcon;
    public Text popupTitle;
    public Text popupDescription;
    public Text popupWeight;
    public Text popupPrice;
    public Button buyButton;
    public Button closeButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        closeButton.onClick.AddListener(HidePopup);
    }

    public void ShowItemPopup(ShopItems item)
    {
        popupIcon.sprite = item.icon;
        popupTitle.text = item.itemName;
        popupDescription.text = item.description;
        popupWeight.text = $"Weight: {item.weight}";
        popupPrice.text = $"Price: {item.buyPrice}G";

        popupPanel.SetActive(true);

        // Handle buy logic
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyItem(item));
    }

    private void BuyItem(ShopItems item)
    {
        Debug.Log($"Bought {item.itemName} for {item.buyPrice}G");
        HidePopup();
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
