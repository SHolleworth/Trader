using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDButton : MonoBehaviour
{
    [SerializeField] Sprite onSprite = null;
    [SerializeField] Sprite offSprite = null;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void On()
    {
        image.sprite = onSprite;
    }

    public void Off()
    {
        image.sprite = offSprite;
    }
}
