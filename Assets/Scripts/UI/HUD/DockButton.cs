using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockButton : MonoBehaviour
{
    [SerializeField] Sprite upSprite = null;
    [SerializeField] Sprite downSprite = null;
    Image image;
    Button button;

    private void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        Enable();
    }

    public void Enable()
    {
        SetUpSprite();
        button.interactable = true;
    }

    public void Disable()
    {
        SetDownSprite();
        button.interactable = false;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void SetUpSprite()
    {
        image.sprite = upSprite;
        Color temp = image.color;
        temp.a = 1;
        image.color = temp;
    }

    private void SetDownSprite()
    {
        image.sprite = downSprite;
        Color temp = image.color;
        temp.a = .5f;
        image.color = temp;
    }

}
