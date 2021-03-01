using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGrandTotalPanel : GrandTotalPanel
{
    public override void SetTotals(float newCost, float newWeight)
    {
        base.SetTotals(newCost, newWeight);
        CheckTotals();
    }

    void CheckTotals()
    {
        if (GrandTotalCost > playerComputer.GetMoney())
        {
            SetCostTextColor(Color.red);
            DisableButton();
        }
        else
        {
            SetCostTextColor(uiColor);
            EnableButton();
        }
        if(playerComputer.GetMaxWeight() < playerComputer.GetWeight() + GrandTotalWeight)
        {
            SetWeightTextColor(Color.red);
            DisableButton();
        }
        else
        {
            SetWeightTextColor(uiColor);
            EnableButton();
        }
    }
}
