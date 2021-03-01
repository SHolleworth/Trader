using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeUI : UI
{
    [SerializeField] GameObject stockBoxBlueprint = null;
    [SerializeField] TradeUI otherUI = null;
    [SerializeField] GrandTotalPanel gtp = null;
    protected List<Item> stock;
    protected List<ShopBox> shopBoxes;
    protected List<Item> transactionList;

    private void Awake()
    {
        stock = new List<Item>();
        shopBoxes = new List<ShopBox>();
        transactionList = new List<Item>();
    }

    public GrandTotalPanel GTP { get { return gtp; } }

    public void Cancel()
    {
        Clear();
        Populate();
    }

    public override void Clear()
    {
        base.Clear();
        foreach (ShopBox box in shopBoxes)
            Destroy(box.gameObject);
        for (int i = 0; i < stock.Count; i++)
        {
            stock[i].Quantity = 0;
            if (stock[i].TotalQuantity <= 0)
                stock.Remove(stock[i]);
        }
        shopBoxes.Clear();
        transactionList.Clear();
        gtp.Clear();
    }

    //Remove items from this list
    //Refresh this screen
    //Add items to other list
    //Refresh the other screen

    protected void LoadStock(List<Item> newStock)
    {
        stock = newStock;
    }

    protected override void Populate()
    {
        foreach (Item item in stock)
        {
            ShopBox shopBox = Instantiate(stockBoxBlueprint, stockBoxBlueprint.transform.parent, false).GetComponent<ShopBox>();
            shopBox.gameObject.SetActive(true);
            shopBox.SetInfoFromItem(item);
            shopBoxes.Add(shopBox);
        }
    }

    public virtual void SendTransaction()
    {
        foreach(ShopBox box in shopBoxes)
        {
            if (box.Quantity > 0)
            {
                box.Item.TotalQuantity = box.Item.TotalQuantity - box.Quantity;
                transactionList.Add(box.Item);
            }
        }
        otherUI.ReceiveTransaction(transactionList);
    }

    //check if in stock
    ////if yes: +1 to current stock, remove from transaction
    ////if no: add to stock list, remove from transaction

    public virtual void ReceiveTransaction(List<Item> newStock)
    {
        bool inStock = false;
        for(int i = 0; i < newStock.Count; i++)
        {
            Item temp = newStock[i];
            foreach(Item stockItem in stock)
            {
                if(temp.Name == stockItem.Name)
                {
                    stockItem.TotalQuantity = stockItem.TotalQuantity + temp.Quantity;
                    inStock = true;
                    break;
                }
            }
            if(!inStock)
            {
                stock.Add(new Item(temp.Name, temp.SpriteName, temp.BuyPrice, temp.SellPrice, temp.Weight, temp.Quantity));
            }
        }
    }

    void PrintTransaction()
    {
        Debug.Log("Transaction List:");
        foreach (Item item in transactionList)
            Debug.Log(item.Name);
    }

    void PrintStock()
    {
        Debug.Log("Current Stock:");
        foreach (Item item in stock)
            Debug.Log("Name: " + item.Name + " Stock: " + item.TotalQuantity);
            
    }
}
