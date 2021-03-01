using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellUI : TradeUI
{
    public void Initialise()
    {
        LoadStock(playerComputer.GetStock());
        Populate();
    }

    public override void SendTransaction()
    {
        base.SendTransaction();
        playerComputer.SetMoney(playerComputer.GetMoney() + GTP.GrandTotalCost);
        playerComputer.SetWeight(playerComputer.GetWeight() - GTP.GrandTotalWeight);
        playerComputer.SetStock(stock);
        Clear();
        Populate();
    }

    public override void ReceiveTransaction(List<Item> newStock)
    {
        base.ReceiveTransaction(newStock);
        playerComputer.SetStock(stock);
        Clear();
        Populate();
    }
}
