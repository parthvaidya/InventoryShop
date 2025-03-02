using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    //public static CurrencyManager Instance { get; private set; }

    //public event Action<int> OnCurrencyChanged; // Observer event

    //private int playerCurrency = 0; // Starting money
    //public TextMeshProUGUI currencyText;

    //private void Awake()
    //{
    //    if (Instance == null) Instance = this;
    //    UpdateCurrencyUI();

    //}

    //public int GetCurrency() => playerCurrency;

    //public bool CanAfford(int amount) => playerCurrency >= amount;

    //public void AddCurrency(int amount)
    //{
    //    playerCurrency += amount;
    //    OnCurrencyChanged?.Invoke(playerCurrency);
    //    UpdateCurrencyUI();// Notify observers
    //}

    //public bool SpendCurrency(int amount)
    //{
    //    if (!CanAfford(amount)) return false;

    //    playerCurrency -= amount;
    //    OnCurrencyChanged?.Invoke(playerCurrency);
    //    UpdateCurrencyUI();
    //    return true;
    //}

    //private void UpdateCurrencyUI()
    //{
    //    if (currencyText != null) // Ensure it's assigned in the UI
    //        currencyText.text = $"Coins: {playerCurrency}";
    //}

    public static CurrencyManager Instance { get; private set; }

    public event Action<int> OnCurrencyChanged;

    private int playerCurrency = 0;
    public TextMeshProUGUI currencyText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        UpdateCurrencyUI();
    }

    public int GetCurrency() => playerCurrency;

    public bool CanAfford(int amount) => playerCurrency >= amount;

    public void AddCurrency(int amount)
    {
        playerCurrency = Mathf.Max(0, playerCurrency + amount);
        OnCurrencyChanged?.Invoke(playerCurrency);
        UpdateCurrencyUI();
    }

    public bool SpendCurrency(int amount)
    {
        if (!CanAfford(amount)) return false;

        playerCurrency -= amount;
        OnCurrencyChanged?.Invoke(playerCurrency);
        UpdateCurrencyUI();
        return true;
    }

    private void UpdateCurrencyUI()
    {
        if (currencyText != null)
            currencyText.text = $"Coins: {playerCurrency}";
    }

    public void ResetGame()
    {
        playerCurrency = 0;
        OnCurrencyChanged?.Invoke(playerCurrency);
        UpdateCurrencyUI();
    }


}
