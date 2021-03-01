using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    Computer computer;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite engineOnSprite = null;
    [SerializeField] Sprite engineOffSprite = null;
    [SerializeField] float money = 0;
    [SerializeField] float maxWeight = 0;
    [SerializeField] float fuel = 0;
    [SerializeField] float maxFuel = 9999;
    float weight;
    bool locked = false;

    private void Awake()
    {
        computer = GetComponent<Computer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerPrefs.SetFloat(StatHandler.maxFuelName, 9999);
    }

    private void Start()
    {
        SetStats();
        if(!StatHandler.gameStarted)
        {
            money = 9999;
            fuel = maxFuel;
            StatHandler.gameStarted = true;
            
        }
    }

    private void SetStats()
    {
        money = PlayerPrefs.GetFloat(StatHandler.moneyName);
        fuel = PlayerPrefs.GetFloat(StatHandler.fuelName);
        weight = computer.CalculateWeight();
    }

    public bool Locked() { return locked; }

    public void Lock() { locked = true; }

    public void Unlock() { locked = false; }

    public float Money { get { return money; } set { money = value; } }

    public float Weight { get { return weight; } set { weight = value; } }
   
    public float MaxWeight { get { return maxWeight; } set { maxWeight = value; } }

    public float Fuel { get { return fuel; } set { fuel = value; } }

    public float MaxFuel { get { return maxFuel; } }

    public void SetSpriteToEngineOn() { spriteRenderer.sprite = engineOnSprite; }

    public void SetSpriteToEngineOff() { spriteRenderer.sprite = engineOffSprite; }
}

