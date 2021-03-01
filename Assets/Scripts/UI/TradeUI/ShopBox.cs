using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopBox : MonoBehaviour
{
    [SerializeField] Image image = null;
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI costText = null;
    [SerializeField] TextMeshProUGUI weightText = null;
    [SerializeField] TextMeshProUGUI quantityText = null;
    [SerializeField] TextMeshProUGUI totalQuantityText = null;
    [SerializeField] AudioClip beep = null;
    [SerializeField] TradeUI tradeUI = null;
    Sprite sprite;
    GrandTotalPanel gtp = null;
    AudioSource audioSource;
    Item item;
    string iName;
    float cost;
    float totalCost;
    float weight;
    float totalWeight;
    int quantity;
    int totalQuantity;

    private void Start()
    {
        gtp = tradeUI.GTP;
        audioSource = GetComponent<AudioSource>();
    }

    public void SetInfoFromItem(Item item)
    {
        this.item = item;
        sprite = item.Sprite;
        SetImage();
        Name = item.Name;
        Cost = item.BuyPrice;
        TotalCost = Cost;
        Weight = item.Weight;
        TotalWeight = Weight;
        Quantity = item.Quantity;
        TotalQuantity = item.TotalQuantity;
    }

    private void SetImage()
    {
        image.sprite = sprite;
    }

    public void SetInfo(string newName, float newCost, float newWeight, int newTotalQuantity, int newQuantity = 0)
    {
        Name = newName;
        Cost = newCost;
        Weight = newWeight;
        Quantity = newQuantity;
        TotalQuantity = newTotalQuantity;
    }

    public Item Item { get { return item; } }

    public string Name
    {
        get { return iName; }
        set { iName = value; nameText.text = value; }
    }

    public float Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    public float TotalCost
    {
        get { return totalCost; }
        set { totalCost = value; costText.text = "" + value; }
    }

    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public float TotalWeight
    {
        get { return totalWeight; }
        set { totalWeight = value; weightText.text = "" + value; }
    }

    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; quantityText.text = "" + value; }
    }

    public int TotalQuantity
    {
        get { return totalQuantity; }
        set { totalQuantity = value; totalQuantityText.text = "" + value; }
    }

    public void IncrementQuantity()
    {
        PlayBeep();
        Quantity = quantity + 1;
        if (Quantity > TotalQuantity)
        {
            Quantity = 0;
            gtp.SetTotals(gtp.GrandTotalCost - Cost * TotalQuantity, gtp.GrandTotalWeight - Weight * TotalQuantity);
        }
        //Add 1
        else
        {
            gtp.SetTotals(gtp.GrandTotalCost + Cost, gtp.GrandTotalWeight + Weight);
        }
        AdjustCostAndWeight();
        UpdateItem();
    }

    public void DecrementQuantity()
    {
        PlayBeep();
        Quantity = quantity - 1;
        if (Quantity < 0)
        {
            Quantity = TotalQuantity;
            gtp.SetTotals(gtp.GrandTotalCost + Cost * TotalQuantity, gtp.GrandTotalWeight + Weight * TotalQuantity);
        }
        //Minus 1
        else
        {
            gtp.SetTotals(gtp.GrandTotalCost - Cost, gtp.GrandTotalWeight - Weight);
        }
        AdjustCostAndWeight();
        UpdateItem();
    }

    private void UpdateItem()
    {
        item.Quantity = Quantity;
    }

    private void PlayBeep()
    {
        audioSource.PlayOneShot(beep);
    }


    private void AdjustCostAndWeight()
    {
        if (Quantity > 0)
        {
            TotalWeight = Weight * Quantity;
            TotalCost = Cost * Quantity;
        }
        else
        {
            TotalWeight = Weight;
            TotalCost = Cost;
        }
    }
}
