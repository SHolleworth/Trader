using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FuelControls : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] creditsText = null;
    [SerializeField] TextMeshProUGUI[] fuelText = null;
    [SerializeField] float credits = 1000;
    [SerializeField] float fuel = 100;
    [SerializeField] float pricePerUnit = 1;
    [SerializeField] float unitsPerSecond = 10;
    float maxFuel;
    int creditInt;
    int fuelInt;
    string creditsZeroText;
    string fuelZeroText;
    bool buying = false;

    private void Start()
    {
        credits = PlayerPrefs.GetFloat(StatHandler.moneyName);
        fuel = PlayerPrefs.GetFloat(StatHandler.fuelName);
        maxFuel = PlayerPrefs.GetFloat(StatHandler.maxFuelName);
        creditInt = (int)credits;
        fuelInt = (int)fuel;
    }

    private void Update()
    {
        SetText(credits, creditsText);
        SetText(fuel, fuelText);
        Refuel();
    }

    public void BuyFuel()
    {
        buying = true;
    }

    public void CancelFuel()
    {
        buying = false;
    }

    public void ExitScreen()
    {
        PlayerPrefs.SetFloat(StatHandler.moneyName, creditInt);
        PlayerPrefs.SetFloat(StatHandler.fuelName, fuelInt);
        SceneManager.LoadScene("01System" + PlayerPrefs.GetInt(StatHandler.idName));
    }

    private void Refuel()
    {
        if (buying && fuel < maxFuel)
        {
            if (credits > 0)
            {
                credits -= (unitsPerSecond * pricePerUnit) * Time.deltaTime;
                fuel += unitsPerSecond * Time.deltaTime;
                creditInt = (int)credits;
                fuelInt = (int)fuel;
            }
            else
            {
                credits = 0;
            }
        }
    }

    private void SetText(float value, TextMeshProUGUI[] text)
    {
        int divider = 1000;
        for (int i = 0; i < 4; i++)
        {
            text[i].text = "" + (int)value / divider;
            value = value % divider;
            divider /= 10;
        }
    }
}
