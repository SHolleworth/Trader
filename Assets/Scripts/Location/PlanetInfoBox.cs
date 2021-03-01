using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetInfoBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI fuelText = null;
    [SerializeField] TextMeshProUGUI loadingText = null;
    [SerializeField] GameObject infoContent = null;
    [SerializeField] GameObject loadingContent = null;
    [SerializeField] Image hourGlass = null;

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetNameText(string newNameText)
    {
        nameText.text = "Name: " + newNameText;
    }

    public void SetFuelText(string newFuelText)
    {
        fuelText.text = "Fuel: " + newFuelText;
    }

    public void SetColor(Color newColor)
    {
        GetComponent<SpriteRenderer>().color = newColor;
        nameText.color = newColor;
        fuelText.color = newColor;
        loadingText.color = newColor;
        hourGlass.color = newColor;
    }

    public void ShowInfoContent()
    {
        loadingContent.SetActive(false);
        infoContent.SetActive(true);
    }

    public void ShowLoadingContent()
    {
        loadingContent.SetActive(true);
        infoContent.SetActive(false);
    }
}
