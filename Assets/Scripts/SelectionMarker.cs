using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMarker : MonoBehaviour
{
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Show() { sr.enabled = true; }

    public void Hide() { sr.enabled = false; }

    public void SetColor(Color newColor) { sr.color = newColor; }
}
