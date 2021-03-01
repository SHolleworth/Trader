using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUI : TradeUI
{
    Trader trader;

    public void Initialise()
    {
        trader = playerComputer.GetTrader();
        LoadStock(trader.Stock);
        Populate();
    }

    public override void SendTransaction()
    {
        base.SendTransaction();
        playerComputer.SetMoney(playerComputer.GetMoney() - GTP.GrandTotalCost);
        playerComputer.SetWeight(playerComputer.GetWeight() + GTP.GrandTotalWeight);
        trader.Stock = stock;
        Clear();
        Populate();
    }

    public override void ReceiveTransaction(List<Item> newStock)
    {
        base.ReceiveTransaction(newStock);
        trader.Stock = stock;
        Clear();
        Populate();
    }
}
