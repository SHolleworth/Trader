using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    string iName;
    Sprite sprite;
    string spriteName;
    float buyPrice;
    float sellPrice;
    float weight;
    int quantity;
    int totalQuantity;

    public Item(string newName, string newSName, float newBPrice, float newSPrice, float newWeight, int newTotalQuantity)
    {
        iName = newName;
        spriteName = newSName;
        sprite = Resources.Load<Sprite>("itemSprites/" + spriteName);
        buyPrice = newBPrice;
        sellPrice = newSPrice;
        weight = newWeight;
        quantity = 0;
        totalQuantity = newTotalQuantity;
    }

    public string Name { get { return iName; } }

    public string SpriteName { get { return spriteName; } }

    public Sprite Sprite { get { return sprite; } }

    public float BuyPrice { get { return buyPrice; } }

    public float SellPrice { get { return sellPrice; } }

    public float Weight { get { return weight; } }

    public int Quantity { get { return quantity; } set { quantity = value; } }

    public int TotalQuantity { get { return totalQuantity; } set { totalQuantity = value; } }
}
