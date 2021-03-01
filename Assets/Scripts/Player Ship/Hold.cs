using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    Computer computer;
    List<Item> stock;

    private void Awake()
    {
        computer = GetComponent<Computer>();
        stock = new List<Item>();
        LoadTestStock();
    }

    public List<Item> Stock { get { return stock; } set { stock = value; } }

    public float CalculateStockWeight()
    {
        float weight = 0;
        foreach(Item item in stock)
        {
            weight += item.Weight * item.TotalQuantity;
        }
        return weight;
    }

    void LoadTestStock()
    {
        stock.Add(new Item("Fish", "fish", 200, 100, 50, 20));
    }
}
