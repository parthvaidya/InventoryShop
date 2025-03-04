using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
   //create a singleton for currency manager
    public static CurrencyManager Instance { get; private set; }

    public event Action<int> OnCurrencyChanged; //make it an action

    private int playerCurrency = 0;
    public TextMeshProUGUI currencyText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        UpdateCurrencyUI();
    }

    public int GetCurrency() => playerCurrency;

    public bool CanAfford(int amount) => playerCurrency >= amount;


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
        //SoundManager.Instance.Play(Sounds.MoneyAdded);

    }


    //reste game once done
    public void ResetGame()
    {
        playerCurrency = 0;
        OnCurrencyChanged?.Invoke(playerCurrency);
        UpdateCurrencyUI();
    }


}
