using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] string lName;
    [SerializeField] protected Computer computer = null;
    [SerializeField] protected TargetRing targetRing = null;
    [SerializeField] PlanetInfoBox planetInfo = null;
    [SerializeField] Color baseColor = Color.red;

    protected virtual void Start()
    {
        lName = gameObject.name;
        planetInfo.SetNameText(lName);
    }

    public virtual void Dock() { }

    public virtual Trader GetTrader() { Debug.Log("Error: Trying to access trader from Location base class."); return null;}

    public void ShowTargetRing() { targetRing.Show(); }

    public void HideTargetRing() { targetRing.Hide(); }

    public void AnimateTargetRing() { targetRing.Animate(); }

    public void ResetTargetRing() { targetRing.Freeze(); }

    public Vector3 GetPosition() { return transform.position; }

    public void EnablePlanetInfo(int contentNumber)
    {
        planetInfo.Enable();
        ShowContentNumber(contentNumber);
    }

    public void DisablePlanetInfo() { planetInfo.Disable(); }

    public void SetFuelInfo(int fuel) { planetInfo.SetFuelText("" + fuel); }

    public void SetUIColor(Color color)
    {
        SetTargetRingColor(color);
        SetPlanetInfoColor(color);
    }

    private void SetTargetRingColor(Color color) { targetRing.SetColor(color); }

    private void SetPlanetInfoColor(Color color) { planetInfo.SetColor(color); }

    private void ShowContentNumber(int contentNumber)
    {
        if (contentNumber == 0)
            planetInfo.ShowInfoContent();
        else if (contentNumber == 1)
            planetInfo.ShowLoadingContent();
        else
            Debug.Log("Content number for planet info not recognised on " + lName + ". Number was " + contentNumber);
    }

    private void OnMouseEnter()
    {
        if (!computer.EngineIsEngaged())
        {
            ShowTargetRing();
            EnablePlanetInfo(1);
            SetUIColor(baseColor);
            computer.SetDestination(this);
        }
    }

    private void OnMouseExit()
    {
        if (!computer.EngineIsEngaged())
        {
            computer.RemoveDestination();
            HideTargetRing();
            ResetTargetRing();
            DisablePlanetInfo();
        }
    }

    private void OnMouseDown()
    {
        if (!computer.EngineIsEngaged())
            DisablePlanetInfo();
            computer.Voyage();
    }
}
