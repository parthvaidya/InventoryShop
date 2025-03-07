using System.Collections.Generic;

public class ShopModel 
{
    public List<ShopItem> items; //list of items

    //create a model
    public ShopModel()
    {
        items = ShopService.Instance.GetAllItems();
    }

    //get items by their types 
    public List<ShopItem> GetItemsByType(ItemType type)
    {
        return type == ItemType.Consumables || type == ItemType.Materials || type == ItemType.Treasures || type == ItemType.Weapons
            ? ShopService.Instance.GetItemsByCategory(type)
            : ShopService.Instance.GetAllItems();
    }


    //reset the game
    public void ResetGame()
    {   
        items.Clear(); // Clear the shop's current items
        items = ShopService.Instance.GetAllItems(); // Repopulate shop items
    }
}
