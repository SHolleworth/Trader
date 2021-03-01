using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    Color disabledColor;
    Color enabledColor;
    Color pressedColor;
    Color hoveredColor;
    TextMeshProUGUI text;
    Button button;
    bool focused = false;
    bool pressed = false;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        disabledColor = new Color(0, .36f, .37f);
        enabledColor = new Color(0, 0.9738421f, 1);
        pressedColor = new Color(0, 1, 0);
        hoveredColor = new Color(0, 7, 7);
    }

    private void Update()
    {
        ColorText();
    }

    private void ColorText()
    {
        if (ButtonEnabled())
        {
            if (pressed)
                SetTextColor(pressedColor);
            else if (focused)
                SetTextColor(hoveredColor);
            else
                SetTextColor(enabledColor);
        }
        else
        {
            SetTextColor(disabledColor);
        }
    }

    public void OnPointerEnter(PointerEventData p)
    {
        focused = true;
    }

    public void OnPointerExit(PointerEventData p)
    {
        focused = false;
    }

    public void OnPointerDown(PointerEventData p)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData p)
    {
        pressed = false;
    }

    private bool ButtonEnabled()
    {
        return button.interactable;
    }

    private void SetTextColor(Color newColor)
    {
        text.color = newColor;
    }
}
