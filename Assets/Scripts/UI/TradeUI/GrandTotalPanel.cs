using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GrandTotalPanel : UI
{
    [SerializeField] TextMeshProUGUI grandTotalCostText = null;
    [SerializeField] TextMeshProUGUI grandTotalWeightText = null;
    [SerializeField] Button transactionButton = null; 
    float grandTotalCost;
    float grandTotalWeight;

    public float GrandTotalCost
    {
        get { return grandTotalCost; }
        set { grandTotalCost = value; grandTotalCostText.text = "" + value; }
    }

    public float GrandTotalWeight
    {
        get { return grandTotalWeight; }
        set { grandTotalWeight = value; grandTotalWeightText.text = "" + value; }
    }

    public virtual void SetTotals(float newCost, float newWeight)
    {
        GrandTotalCost = newCost;
        GrandTotalWeight = newWeight;
    }

    public override void Clear()
    {
        base.Clear();
        SetTotals(0, 0);
    }

    protected void EnableButton() { transactionButton.interactable = true; }

    protected void DisableButton() { transactionButton.interactable = false; }

    protected void SetCostTextColor(Color newColor) { grandTotalCostText.color = newColor; }

    protected void SetWeightTextColor(Color newColor) { grandTotalWeightText.color = newColor; }
}
