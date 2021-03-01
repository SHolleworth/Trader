using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeOverlay : UI
{
    [SerializeField] HUD hud = null;
    [SerializeField] BuyUI buyUI = null;
    [SerializeField] SellUI sellUI = null;
    [SerializeField] AudioClip beep = null;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Open()
    {
        base.Open();
        buyUI.Clear();
        sellUI.Clear();
        buyUI.Initialise();
        sellUI.Initialise();
        hud.DisableDock();
    }

    public override void Close()
    {
        base.Close();
        hud.EnableDock();
    }

    void PlayBeep()
    {
        audioSource.PlayOneShot(beep);
    }
}
