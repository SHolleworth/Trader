using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : Location
{
    [SerializeField] int id;
    private void Start()
    {
        PlayerPrefs.SetInt(StatHandler.idName, id);
    }

    public override void Dock()
    {
        SceneManager.LoadScene("03GalaxyMap");
    }
}
