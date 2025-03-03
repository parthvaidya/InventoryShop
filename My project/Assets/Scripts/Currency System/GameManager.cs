using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private ShopController shopController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager persistent
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetDependencies(InventoryController inventory, CurrencyManager currency , ShopController shopController)
    {
        this.inventoryController = inventory;
        this.currencyManager = currency;
        this.shopController = shopController;
    }

    public void EndGame()
    {
        Debug.Log("Game Over! Resetting inventory and currency...");
        currencyManager.ResetGame();
        inventoryController.ResetGame();
        shopController.ResetGame();
        
    }
}
