using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    //create instance
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

        //set active false initially
        popupPanel.SetActive(false);
        itemSoldPanel.SetActive(false);

        yesButton.onClick.AddListener(Confirm);
        noButton.onClick.AddListener(ClosePopup);

        if (soldMessageText != null)
        {
            soldMessageText.gameObject.SetActive(false); // Hide initially
        }
    }

    //show confirmation message
    public void ShowConfirmation(string message, System.Action confirmAction)
    {
        messageText.text = message;
        onConfirm = confirmAction;
        popupPanel.SetActive(true);
       
        
    }

    //confirm the message
    private void Confirm()
    {
        SoundManager.Instance.Play(Sounds.ClickItem);
        onConfirm?.Invoke();
        ShowSoldMessage();
        ClosePopup();
    }

    //show the sold popup 
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

    //delay using coroutine
    private IEnumerator HideSoldPanelAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        itemSoldPanel.SetActive(false);
    }

    //close the popup panel
    private void ClosePopup()
    {
        SoundManager.Instance.Play(Sounds.ClickItem);
        popupPanel.SetActive(false);
        
    }
}
