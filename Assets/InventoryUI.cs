using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UI
{
    [SerializeField] InventoryBox inventoryBoxBlueprint = null;
    List<Item> stock;
    List<InventoryBox> boxes;

    private void Awake()
    {
        boxes = new List<InventoryBox>();
    }

    public override void Open()
    {
        base.Open();
        SetStock();
        Clear();
        Populate();
    }

    public override void Close()
    {
        Clear();
        base.Close();
    }

    private void SetStock()
    {
        stock = playerComputer.GetStock();
    }

    protected override void Populate()
    {
        foreach(Item item in stock)
        {
            InventoryBox newBox = Instantiate(inventoryBoxBlueprint, inventoryBoxBlueprint.transform.parent);
            newBox.Enable();
            newBox.Item = item;
            boxes.Add(newBox);
        }
    }

    public override void Clear()
    {
        for (int i = 0; i < boxes.Count; i++)
            Destroy(boxes[i].gameObject);
        boxes.Clear();
    }
}
