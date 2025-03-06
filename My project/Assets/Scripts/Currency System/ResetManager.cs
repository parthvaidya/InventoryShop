using UnityEngine;

public class ResetManager : MonoBehaviour
{
   
    public static ResetManager Instance { get; private set; }

    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private ShopController shopController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep ResetManager persistent
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    //set the dependencies
    public void SetDependencies(InventoryController inventory, CurrencyManager currency , ShopController shopController)
    {
        this.inventoryController = inventory;
        this.currencyManager = currency;
        this.shopController = shopController;
    }

    // endgame to reset everything after the session
    public void EndGame()
    {
        Debug.Log("Game Over! Resetting inventory and currency...");
        currencyManager.ResetGame();
        inventoryController.ResetGame();
        shopController.ResetGame();  
    }
}
