using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] Computer computer = null;
    [SerializeField] DockButton dockButton = null;
    [SerializeField] TextMeshProUGUI moneyText = null;
    [SerializeField] TextMeshProUGUI fuelText = null;
    [SerializeField] TextMeshProUGUI weightText = null;
    [SerializeField] TextMeshProUGUI dateText = null;
    float time = 0;
    int days;
    int months;
    int years;

    private void Awake()
    {
        days = 1;
        months = 1;
        years = 3000;
        time = days;
    }

    private void Update()
    {
        IncreaseTime();
        UpdateStats();
    }

    public void Dock()
    {
        computer.Dock();
    }

    public void EnableDock()
    {
        dockButton.Enable();
    }

    public void DisableDock()
    {
        dockButton.Disable();
    }

    public void HideDock()
    {
        dockButton.Hide();
    }

    public void ShowDock()
    {
        dockButton.Show();
    }

    private void UpdateStats()
    {
        moneyText.text = "Money: " + computer.GetMoney();
        fuelText.text = "Fuel: " + computer.GetFuel();
        weightText.text = "Weight : " + computer.GetWeight() + "/" + computer.GetMaxWeight();
        dateText.text = "Date : " + days + "/" + months + "/" + years;
    }

    private void IncreaseTime()
    {
        if(computer.EngineIsEngaged())
        {
            time += Time.deltaTime;
            days = (int)time;
            if (days > 30)
            {
                time -= 29;
                months++;
            }
            if (months > 12)
            {
                months = 1;
                years++;
            }
        }
    }
}
