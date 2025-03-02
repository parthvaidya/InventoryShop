using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private CurrencyManager currencyManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over! Resetting inventory and currency...");
        currencyManager.ResetGame();
        inventoryController.ResetGame();
    }
}
