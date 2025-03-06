using System;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
   //create a singleton for currency manager
    public static CurrencyManager Instance { get; private set; }
    public event Action<int> OnCurrencyChanged; //make it an action
    private int playerCurrency = 0;
    public TextMeshProUGUI currencyText;
    public int GetCurrency() => playerCurrency;
    public bool CanAfford(int amount) => playerCurrency >= amount; 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep it persistent
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        UpdateCurrencyUI();
    }

    //add currency
    public void AddCurrency(int amount)
    {
        playerCurrency = Mathf.Max(0, playerCurrency + amount);
        OnCurrencyChanged?.Invoke(playerCurrency);
        UpdateCurrencyUI();
    }

    //spend currency
    public bool SpendCurrency(int amount)
    {
        if (!CanAfford(amount)) return false;
        playerCurrency -= amount; //reduce amount
        OnCurrencyChanged?.Invoke(playerCurrency);
        UpdateCurrencyUI();
        return true;
    }

    //update currency
    private void UpdateCurrencyUI()
    {
        if (currencyText != null)
            currencyText.text = $"Coins: {playerCurrency}";
    }

    //reset game once done
    public void ResetGame()
    {
        playerCurrency = 0;
        OnCurrencyChanged?.Invoke(playerCurrency);
        UpdateCurrencyUI();
    }
}
