using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { All, Consumables, Materials, Treasures, Weapons }
public enum Rarity { VeryCommon, Common, Rare, Epic, Legendary }

[CreateAssetMenu(fileName = "NewShopItemCollection", menuName = "Shop/ItemCollection")]
public class ShopItemCollection : ScriptableObject
{
    public List<ShopItem> items = new List<ShopItem>();
}

[System.Serializable]
public class ShopItem
{

    public string itemName;
    public string description;
    public ItemType itemType;
    public Rarity rarity;
    public int quantity;
    public float weight;
    public int buyPrice;
    public int sellPrice;
    public Sprite icon;
}
