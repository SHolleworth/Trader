using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    Computer computer;
    List<GameObject> path;
    int pathIndex = 0;
    bool engaged = false;
    float step;
    [SerializeField] float speed = 5;
    //for accel and decel
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float acceleration = 5;
    [SerializeField] float deceleration = 5;

    private void Start()
    {
        computer = GetComponent<Computer>();
    }

    private void Update()
    {
        Voyage();
    }

    public void SetSpeed(float newSpeed) { speed = newSpeed; }

    public bool IsEngaged() { return engaged; }

    public void Engage(List<GameObject> newPath)
    {
        SetPath(newPath);
        engaged = true;
        speed = minSpeed;
        computer.EnableEngineSprite();
        computer.DisableDock();
    }
    
    private void Disengage()
    {
        engaged = false;
        computer.DisableEngineSprite();
        computer.EnableDock();
    }

    private void Voyage()
    {
        if (engaged)
        {
            AdjustAcceleration();
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, path[pathIndex].transform.position, step);
            RotateTowards(path[pathIndex].transform.position);
            if (Vector3.Distance(transform.position, path[pathIndex].transform.position) < 0.1f)
            {
                Destroy(path[pathIndex]);
                pathIndex++;
                computer.UseFuel(1);
            }
            if (Vector3.Distance(transform.position, path[path.Count - 1].transform.position) < 0.1f)
            {
                Disengage();
                computer.Orbit();
            }
        }
    }

    private void AdjustAcceleration()
    {
        if (Vector3.Distance(transform.position, path[path.Count - 1].transform.position) < 4)
        {
            Decelerate();
        }
        else
        {
            Accelerate();
        }
    }

    private void Decelerate()
    {
        if (speed > minSpeed)
            speed -= deceleration * Time.deltaTime;
        if (speed < minSpeed)
            speed = minSpeed;
    }

    private void Accelerate()
    {
        if (speed < maxSpeed)
            speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed)
            speed = maxSpeed;
    }

    private void SetPath(List<GameObject> newPath)
    {
        path = newPath;
        pathIndex = 0;
    }

    private void RotateTowards(Vector3 target)
    {
        Vector2 relativePosition = target - transform.position;
        float angle = (Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg) - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .1f);
    }
}