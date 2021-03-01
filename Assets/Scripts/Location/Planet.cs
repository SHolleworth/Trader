using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Location
{
    Trader trader;
    [SerializeField] TradeOverlay tradeOverlay = null;

    protected override void Start()
    {
        base.Start();
        trader = GetComponent<Trader>();
    }

    public override Trader GetTrader() { return trader; }

    public override void Dock()
    {
        tradeOverlay.Open();
    }
}
