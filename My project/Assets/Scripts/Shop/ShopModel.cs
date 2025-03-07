using System.Collections.Generic;

public class ShopModel 
{
    //List of shop items
    public List<ShopItem> items;

    //STore the items
    public ShopModel(List<ShopItem> initialItems)
    {
        items = new List<ShopItem>(initialItems);
    }

    //Get the item based on its types
    public List<ShopItem> GetItemsByType(ItemType type)
    {
        if (type == ItemType.All)
        {
            return new List<ShopItem>(items);
        }
        return items.FindAll(item => item.itemType == type);
    }

    //Reset the game
    public void ResetGame(List<ShopItem> initialItems)
    {
        items.Clear();
        items.AddRange(initialItems);
    }
}
