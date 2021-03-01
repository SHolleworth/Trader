using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public void EnableSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DisableSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
