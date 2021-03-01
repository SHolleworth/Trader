using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] protected Computer playerComputer = null;
    [SerializeField] protected Color uiColor = Color.magenta;

    public virtual void Open() { gameObject.SetActive(true); }

    public virtual void Close() { gameObject.SetActive(false); }

    protected virtual void Populate() { }

    public virtual void Clear() { }
}
