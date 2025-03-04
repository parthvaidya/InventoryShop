using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    public static ConfirmationPopup Instance;

    [Header("UI References")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button yesButton, noButton;
    [SerializeField] private GameObject itemSoldPanel;

    [SerializeField] private TextMeshProUGUI soldMessageText;
    [SerializeField] private GameObject invenryPopUp;
    

    private System.Action onConfirm;

    private void Awake()
    {


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }

        // Ensure the popup is properly initialized even if inactive
        if (popupPanel == null)
        {
            Debug.LogError("ConfirmationPopup: popupPanel is not assigned in the Inspector!");
        }

        
        popupPanel.SetActive(false);
        itemSoldPanel.SetActive(false);

        yesButton.onClick.AddListener(Confirm);
        noButton.onClick.AddListener(ClosePopup);

        if (soldMessageText != null)
        {
            soldMessageText.gameObject.SetActive(false); // Hide initially
        }
    }

    public void ShowConfirmation(string message, System.Action confirmAction)
    {
        messageText.text = message;
        onConfirm = confirmAction;
        popupPanel.SetActive(true);
       
        //tickImage.gameObject.SetActive(true); // Show tick next to "Yes"
    }

    private void Confirm()
    {
        SoundManager.Instance.Play(Sounds.ClickItem);
        onConfirm?.Invoke();
        ShowSoldMessage();
        ClosePopup();
    }

    private void ShowSoldMessage()
    {
        SoundManager.Instance.Play(Sounds.PopupMusic);
        if (soldMessageText != null)
        {
            soldMessageText.text = "Item Sold!"; // Set message text
            soldMessageText.gameObject.SetActive(true);
            itemSoldPanel.SetActive(true);
            StartCoroutine(HideSoldPanelAfterDelay());// Show message

            
        }
    }

    private IEnumerator HideSoldPanelAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        itemSoldPanel.SetActive(false);
    }

    public void ClosePopup()
    {
        SoundManager.Instance.Play(Sounds.ClickItem);
        popupPanel.SetActive(false);
        
    }
}
