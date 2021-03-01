using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] HUD hud = null;
    PlayerShip ship;
    Navigation navigation;
    Orbiter orbiter;
    Engine engine;
    Hold hold;

    private void Awake()
    {
        ship = GetComponent<PlayerShip>();
        navigation = GetComponent<Navigation>();
        orbiter = GetComponent<Orbiter>();
        engine = GetComponent<Engine>();
        hold = GetComponent<Hold>();
    }

    //HUD interface
    public void EnableDock() { hud.EnableDock(); }

    public void DisableDock() { hud.DisableDock(); }

    //trade/hold interface
    public Trader GetTrader() { return GetOrigin().GetTrader(); }

    public List<Item> GetStock() { return hold.Stock; }

    public void SetStock(List<Item> newStock) { hold.Stock = newStock; }

    public float CalculateWeight() { return hold.CalculateStockWeight(); }

    //ship interface
    public float GetMoney() { return ship.Money; }

    public float GetFuel() { return ship.Fuel; }

    public float GetMaxFuel() { return ship.MaxFuel; }

    public float GetWeight() { return ship.Weight; }

    public float GetMaxWeight() { return ship.MaxWeight; }

    public void SetMoney(float newMoney) { ship.Money = newMoney; }

    public void SetFuel(float newFuel) { ship.Fuel = newFuel; }

    public void SetWeight(float newWeight) { ship.Weight = newWeight; }

    public void LockShip() { ship.Lock(); }

    public void UnlockShip() { ship.Unlock(); }

    public bool ShipIsLocked() { return ship.Locked(); }

    public void EnableEngineSprite() { ship.SetSpriteToEngineOn(); }

    public void DisableEngineSprite() { ship.SetSpriteToEngineOff(); }

    //navigation interface
    public Location GetOrigin() { return navigation.GetOrigin(); }

    public void MakeDestinationIntoOrigin() { navigation.MakeDestinationIntoOrigin(); }

    public void SetDestination(Location destination) { navigation.Target(destination); }

    public void RemoveDestination() { navigation.CancelTarget(); }

    public int GetFuelCost() { return navigation.GetFuelCost(); }

    public void Dock()
    {
        GetOrigin().Dock();
        LockShip();
    }

    //engine interface
    public bool EngineIsEngaged() { return engine.IsEngaged(); }

    public void UseFuel(int fuelSpent) { ship.Fuel = (ship.Fuel - fuelSpent); }

    //multi interface
    public void Voyage()
    {
        if(!ShipIsLocked())
        {
            orbiter.StopOrbiting();
            engine.Engage(navigation.GetPath());
        }
    }

    public void Orbit()
    {
        MakeDestinationIntoOrigin();
        navigation.GetOrigin().ResetTargetRing();
        navigation.GetOrigin().DisablePlanetInfo();
        navigation.CancelTarget();
        orbiter.SetOrbit(navigation.GetOrigin().gameObject);
        orbiter.StartOrbiting();
    }
}
