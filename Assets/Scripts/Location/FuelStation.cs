using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelStation : Location
{
    [SerializeField] SceneHandler sceneHandler = null;

    public override void Dock()
    {
        base.Dock();
        sceneHandler.LoadScene("02FuelStation");
    }
}
