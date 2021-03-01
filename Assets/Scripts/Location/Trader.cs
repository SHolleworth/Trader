using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    [SerializeField] string tName = "Missing";
    [SerializeField] TextAsset stockCSV = null;
    List<Item> stock;

    private void Awake()
    {
        LoadStockFromCSV();
    }

    public string Name { get { return tName; } }

    public List<Item> Stock { get { return stock; } set { stock = value; } }

    private void LoadStockFromCSV()
    {
        string[] newLineSeperatedStockText = stockCSV.text.Split('\n');
        stock = new List<Item>();
        foreach (string commaSeperatedItemText in newLineSeperatedStockText)
        {
            if (commaSeperatedItemText[0] != '#')
            {
                string[] itemText = commaSeperatedItemText.Split(',');
                string iName = itemText[0];
                string spriteName = itemText[1];
                float buyPrice = float.Parse(itemText[2]);
                float sellPrice = float.Parse(itemText[3]);
                float weight = float.Parse(itemText[4]);
                int quantity = int.Parse(itemText[5]);
                stock.Add(new Item(iName, spriteName, buyPrice, sellPrice, weight, quantity));
            }
        }
    }
}
