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
    [SerializeField] private TextMeshProUGUI soldMessageText;
    [SerializeField] private GameObject invenryPopUp;
    //public Image tickImage; // The tick next to "Yes"

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
        ShowSoldMessage();
        //tickImage.gameObject.SetActive(true); // Show tick next to "Yes"
    }

    private void Confirm()
    {
        onConfirm?.Invoke();
        ClosePopup();
    }

    private void ShowSoldMessage()
    {
        if (soldMessageText != null)
        {
            soldMessageText.text = "Item Sold!"; // Set message text
            soldMessageText.gameObject.SetActive(true); // Show message

            // Hide message after 2 seconds
            StartCoroutine(HideSoldMessageAfterDelay());
        }
    }

    private IEnumerator HideSoldMessageAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        soldMessageText.gameObject.SetActive(false); // Hide message
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
        //invenryPopUp.SetActive(false);
    }
}
