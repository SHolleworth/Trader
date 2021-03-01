using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDButtons : MonoBehaviour
{
    [SerializeField] HUD hud = null;
    [SerializeField] MapScreen mapScreen = null;
    [SerializeField] InventoryUI inventory= null;
    [SerializeField] TradeOverlay trade = null;

    [SerializeField] HUDButton mapButton = null;
    [SerializeField] HUDButton inventoryButton = null;
    [SerializeField] HUDButton questButton = null;
    [SerializeField] HUDButton newsButton = null;

    private void Start()
    {
        OpenMap();
    }

    public void MapButton()
    {
        OpenMap();
        CloseInventory();
    }

    public void InventoryButton()
    {
        CloseMap();
        OpenInventory();
    }

    void OpenMap()
    {
        mapScreen.Enable();
        mapButton.On();
        ShowDock();
    }

    void CloseMap()
    {
        DisableTrade();
        mapButton.Off();
        HideDock();
        mapScreen.Disable();
    }

    void HideDock()
    {
        hud.HideDock();
    }

    void ShowDock()
    {
        hud.ShowDock();
    }

    void DisableTrade()
    {
        trade.Close();
    }

    void OpenInventory()
    {
        inventory.Open();
        inventoryButton.On();
    }

    void CloseInventory()
    {
        inventory.Close();
        inventoryButton.Off();
    }
}
