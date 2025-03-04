using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { All, Consumables, Materials, Treasures, Weapons } //item types
public enum Rarity { VeryCommon, Common, Rare, Epic, Legendary } //Item rarity

[CreateAssetMenu(fileName = "NewShopItemCollection", menuName = "Shop/ItemCollection")]
public class ShopItemCollection : ScriptableObject
{
    public List<ShopItem> items = new List<ShopItem>();
}

//scritptable items for shop Items

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
