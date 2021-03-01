using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnimation : MonoBehaviour
{
    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        transform.Rotate(new Vector3(0, 0, -.2f));
    }
}
