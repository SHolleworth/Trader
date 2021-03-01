using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRing : MonoBehaviour
{
    [Range(-100, 100)] public float rotationSpeed;
    bool animated = false;
    protected Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (animated)
        {
            Rotate();
        }
    }

    public void Show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    public void Animate()
    {
        animated = true;
    }

    public void Freeze()
    {
        animated = false;
        ResetAnimation();
        ResetColor();
        Hide();
    }

    public void ResetRotation()
    {
        transform.rotation = originalRotation;
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    private void ResetAnimation()
    {
        transform.rotation = originalRotation;
    }

    void ResetColor()
    {
        SetColor(Color.white);
    }

}
