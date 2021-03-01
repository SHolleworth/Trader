using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelButton : MonoBehaviour
{
    [SerializeField] FuelControls fuelControls = null;
    [SerializeField] Sprite upSprite = null;
    [SerializeField] Sprite downSprite = null;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        spriteRenderer.sprite = downSprite;
        fuelControls.BuyFuel();
    }

    private void OnMouseUp()
    {
        spriteRenderer.sprite = upSprite;
        fuelControls.CancelFuel();
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = upSprite;
        fuelControls.CancelFuel();
    }
}
