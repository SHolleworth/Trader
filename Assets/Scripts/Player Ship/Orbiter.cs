using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    Computer computer;

    [SerializeField] float orbitDistance = .5f;
    [Range(1, 20)][SerializeField] float orbitSpeed = 1.0f;
    Vector3[] pointArray;
    int pointIndex = 0;
    float x;
    float y;
    int theta = 0;
    float h;
    float k;
    float r;
    [SerializeField] int step = 9;
    bool orbiting = false;

    private void Start()
    {
        computer = GetComponent<Computer>();
        pointArray = new Vector3[(360 / step)];
        SetOrbit(computer.GetOrigin().gameObject);
        StartOrbiting();
    }

    private void Update()
    {
        if (orbiting)
            Orbit();
    }

    private void DrawCircle()
    {
        int i = 0;
        while (theta < 450)
        {
            x = h + r * Mathf.Cos(Mathf.Deg2Rad * theta);
            y = k + r * Mathf.Sin(Mathf.Deg2Rad * theta);
            pointArray[i] = new Vector3(x, y);
            i++;
            theta += step;
        }
    }

    private void Orbit()
    {
        RotateTowards(pointArray[pointIndex]);
        transform.position = Vector3.MoveTowards(transform.position, pointArray[pointIndex], orbitSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, pointArray[pointIndex]) < 0.01f)
        {
            pointIndex++;
        }
        if (pointIndex == pointArray.Length)
        {
            pointIndex = 0;
        }
    }

    public void StartOrbiting()
    {
        orbiting = true;
    }

    public void StopOrbiting()
    {
        orbiting = false;
    }

    public void SetOrbit(GameObject planet)
    {
        pointIndex = 0;
        theta = 90;
        h = planet.transform.position.x;
        k = planet.transform.position.y;
        r = planet.GetComponent<CircleCollider2D>().radius + orbitDistance;
        DrawCircle();
    }

    private void RotateTowards(Vector3 target)
    {
        Vector2 relativePosition = target - transform.position;
        float angle = (Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg) - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .05f);
    }
}

