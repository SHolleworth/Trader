using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryBox : MonoBehaviour
{
    [SerializeField] Image image = null;
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI costText = null;
    [SerializeField] TextMeshProUGUI weightText = null;
    [SerializeField] TextMeshProUGUI totalCostText = null;
    [SerializeField] TextMeshProUGUI totalWeightText = null;
    [SerializeField] TextMeshProUGUI quantityText = null;
    string iName;
    Sprite sprite;
    float weight;
    float cost;
    float totalWeight;
    float totalCost;
    float quantity;

    public Item Item
    {
        set
        {
            Name = value.Name;
            Sprite = value.Sprite;
            Weight = value.Weight;
            Cost = value.BuyPrice;
            Quantity = value.TotalQuantity;
            TotalWeight = Weight * Quantity;
            TotalCost = Cost * Quantity;
        }
    }

    string Name { set { iName = value; nameText.text = value + ""; } }

    Sprite Sprite { set { sprite = value; image.sprite = value; } }

    float Weight { set { weight = value; weightText.text = value + ""; } get { return weight; } }

    float Cost { set { cost = value; costText.text = value + ""; } get { return cost; } }

    float TotalWeight { set { totalWeight = value; totalWeightText.text = value + ""; } }

    float TotalCost { set { totalCost = value; totalCostText.text = value + ""; } }

    float Quantity { set { quantity = value; quantityText.text = value + ""; } get { return quantity; } }



    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
