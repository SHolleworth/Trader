using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation: MonoBehaviour
{
    Computer computer;

    AudioSource audioSource;
    [SerializeField] AudioClip beep = null;
    [SerializeField] AudioClip confirmation = null;
    [SerializeField] AudioClip denial = null;

    //point 0
    [SerializeField] Location origin = null;
    //point 1
    Location destination;
    [Range(.1f, 20f)] public float lineDrawSpeed;

    [SerializeField] GameObject pathPoint = null;
    List<GameObject> pathPointDeletionList;
    List<GameObject> pathPointFinalList;

    //Quadratic Bezier points for drawing a curved path
    Vector3 point0;
    Vector3 point1;
    Vector3 oppositePoint1;
    Vector3 point2;
    //t Bezier value
    float x;
    //ammount to offset curve when recalculating path
    [SerializeField] float recalculationIncrement = .5f;
    //distance between origin and destination
    float distance;
    //whether the targetted planet can be reached with current supplies
    bool reachable = false;

    //control the amount of points in a path
    float counter;
    float counterIncrement;
    [SerializeField] float counterAdjustment = 4;

    //colors for path ui
    [SerializeField] Color successColor = Color.green;
    [SerializeField] Color failureColor = Color.red;

    private void Awake()
    {
        computer = GetComponent<Computer>();
        pathPointDeletionList = new List<GameObject>();
        pathPointFinalList = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
    }

    public List<GameObject> GetPath() { return pathPointFinalList; }

    public Location GetOrigin() { return origin; }

    public int GetFuelCost()
    {
        return pathPointFinalList.Count;
    }

    public void MakeDestinationIntoOrigin() { origin = destination; }

    //Function activated by mousing over planets
    public void Target(Location target)
    {
        SetDestination(target);
        FindDistance();
        SetBezierPoints();
        FindPath();
        StartCoroutine(DrawPath());
    }

    void SetDestination(Location newDest)
    {
        destination = newDest;
    }

    void FindDistance()
    {
        distance = Vector3.Distance(origin.GetPosition(), destination.GetPosition());
        counterIncrement = (1 / distance) / counterAdjustment;
    }

    private void SetBezierPoints()
    {
        point0 = origin.GetPosition();
        point2 = destination.GetPosition();
        point1 = new Vector3(point0.x, point2.y);
        oppositePoint1 = new Vector3(point2.x, point0.y);
    }


    //Finds an optimal path around the star
    void FindPath()
    {
        char quad = FindQuadrantOfDestination();
        if (quad != 'x')
        {
            GameObject pointAlongTheLine;
            Collider2D[] pointCollisionArray = new Collider2D[1];
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            contactFilter2D.NoFilter();

            pathPoint.GetComponent<PathPoint>().EnableSpriteRenderer();
            pathPoint.GetComponent<CircleCollider2D>().enabled = enabled;

            while (true)
            {
                pointAlongTheLine = CalculateAndDrawNextPoint();
                pointAlongTheLine.GetComponent<Collider2D>().OverlapCollider(contactFilter2D, pointCollisionArray);

                if (pointCollisionArray[0] != null)
                {
                    if (pointCollisionArray[0].tag == "Star")
                    {
                        switch (quad)
                        {
                            case 'a':
                                RecalculateQuadA();
                                break;

                            case 'b':
                                RecalculateQuadB();
                                break;

                            case 'c':
                                RecalculateQuadC();
                                break;

                            case 'd':
                                RecalculateQuadD();
                                break;

                            default:
                                break;
                        }
                        //DrawPoint1();
                        SwapPathDirection();
                        DeletePath();
                        counter = 0;
                    }
                    else if (pointCollisionArray[0] == destination.GetComponent<Collider2D>())
                    {
                        break;
                    }
                    pointCollisionArray[0] = null;
                }

            }
        }
        else
        {
            Debug.Log("Error, could not locate quadrant.");
        }
        CancelTarget();
    }

    private void RecalculateQuadA()
    {
        if (point1.x >= origin.GetPosition().x)
        {
            //Debug.Log("Increased x");
            point1.x += recalculationIncrement;
        }
        else
        {
            //Debug.Log("Decreased x");
            point1.x -= recalculationIncrement;
        }
    }

    private void RecalculateQuadC()
    {
        if (point1.x >= origin.GetPosition().x)
        {
            //Debug.Log("Increased x");
            point1.x += recalculationIncrement;
        }
        else
        {
            //Debug.Log("Decreased x");
            point1.x -= recalculationIncrement;
        }
    }

    private void RecalculateQuadB()
    {
        if (point1.y >= origin.GetPosition().y)
        {
            //Debug.Log("Increased y");
            point1.y += recalculationIncrement;
        }
        else
        {
            //Debug.Log("Decreased y");
            point1.y -= recalculationIncrement;
        }
    }

    private void RecalculateQuadD()
    {
        if (point1.y > origin.GetPosition().y)
        {
            //Debug.Log("Increased y");
            point1.y += recalculationIncrement;
        }
        else
        {
            //Debug.Log("Decreased y");
            point1.y -= recalculationIncrement;
        }
    }

    private void SwapPathDirection()
    {
        Vector3 temp = point1;
        point1 = oppositePoint1;
        oppositePoint1 = temp;
    }

    //Praise be to the master function, devourer of spare time, conquerer of free afternoons
    private char FindQuadrantOfDestination()
    {
        char quadrant = 'x';
        Vector3 n = origin.GetPosition();
        Vector3 i = destination.GetPosition();
        float cAscending = n.y - n.x;
        float cDescending = n.y + n.x;

        if (i.x > n.x)
        {
            if (i.y > n.y)
            {
                if (i.y < (i.x + cAscending))
                {
                    quadrant = 'b';
                }
                else if (i.y >= (i.x + cAscending))
                {
                    quadrant = 'a';
                }
            }
            else if (i.y < n.y)
            {
                if (i.y >= (-i.x + cDescending))
                {
                    quadrant = 'b';
                }
                else if (i.y < (-i.x + cDescending))
                {
                    quadrant = 'c';
                }
            }
        }
        else if (i.x < n.x)
        {
            if (i.y > n.y)
            {
                if (i.y <= (-i.x + cDescending))
                {
                    quadrant = 'd';
                }
                else if (i.y > (-i.x + cDescending))
                {
                    quadrant = 'a';
                }
            }
            else if (i.y < n.y)
            {
                if (i.y > (i.x - cAscending))
                {
                    quadrant = 'd';
                }
                else if (i.y <= (i.x - cAscending))
                {
                    quadrant = 'c';
                }
            }
        }
        return quadrant;
    }

    private void DrawPoint1()
    {
        pathPoint.GetComponent<PathPoint>().EnableSpriteRenderer();
        Instantiate(pathPoint, point1, Quaternion.identity).GetComponent<SpriteRenderer>().color = Color.red;
        pathPoint.GetComponent<PathPoint>().DisableSpriteRenderer();
    }

    private GameObject CalculateAndDrawNextPoint()
    {
        counter += counterIncrement;
        x = Mathf.Lerp(0, 1, counter);

        Vector3 pointAlongTheLine = QuadraticBezierPoint(x, point0, point1, point2);

        GameObject newPoint = Instantiate(pathPoint, pointAlongTheLine, Quaternion.identity);

        pathPointDeletionList.Add(newPoint);

        return newPoint;
    }

    Vector3 QuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        Vector3 pA = LinearBezierPoint(t, p0, p1);
        Vector3 pB = LinearBezierPoint(t, p1, p2);
        return LinearBezierPoint(t, pA, pB);
    }

    private Vector3 LinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return (1 - t) * p0 + t * p1;
    }

    IEnumerator DrawPath()
    {
        Vector3 pointAlongTheLine;
        pathPoint.GetComponent<PathPoint>().EnableSpriteRenderer();
        pathPoint.GetComponent<CircleCollider2D>().enabled = false;
        GameObject newPoint;

        while (true)
        {
            //draw point
            newPoint = CalculateAndDrawNextPoint();
       
            pathPointFinalList.Add(newPoint);
            pointAlongTheLine = newPoint.transform.position;

            //play beep
            //if(!audioSource.isPlaying)
                PlaySound(beep, 0.03f);

            if (pointAlongTheLine == destination.GetPosition())
            {
                CheckIfLocationCanBeReached();
                TrimPoint(newPoint);
                break;
            }

            TrimPoint(newPoint);

            yield return new WaitForSeconds(lineDrawSpeed);
        }
    }

    private void TrimPoint(GameObject point)
    {
            if (destination.GetComponent<Collider2D>().bounds.Contains(point.transform.position))
            {
                pathPointFinalList.Remove(point);
                Destroy(point);
            }
            else if (origin.GetComponent<Collider2D>().bounds.Contains(point.transform.position))
            {
                pathPointFinalList.Remove(point);
                Destroy(point);
            }
    }

    private void CheckIfLocationCanBeReached()
    {
        //Color 
        if(origin == destination)
        {
            reachable = true;
            computer.LockShip();
        }
        else if (CalculateFuelUse(computer.GetFuel()) >= 0)
        {
            reachable = true;
            computer.UnlockShip();
        }
        else
        {
            reachable = false;
            computer.LockShip();
        }

        if (reachable)
        {
            foreach (var point in pathPointFinalList)
                point.GetComponent<SpriteRenderer>().color = successColor;

            destination.SetUIColor(successColor);
            destination.AnimateTargetRing();
            
            PlaySound(confirmation, 1.5f);
        }
        else
        {
            foreach (var point in pathPointFinalList)
                point.GetComponent<SpriteRenderer>().color = failureColor;
            destination.SetUIColor(failureColor);
            PlaySound(denial);
        }

        destination.EnablePlanetInfo(0);
        destination.SetFuelInfo(pathPointFinalList.Count);
    }

    private void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        audioSource.PlayOneShot(clip, volume);
    }

    public float CalculateFuelUse(float fuel)
    {
        float newFuel = fuel - pathPointFinalList.Count;
        return newFuel;
    }

    public void CancelTarget()
    {
        StopAllCoroutines();
        DeletePath();
        ResetFinalPath();
        computer.LockShip();
    }

    private void ResetFinalPath()
    {
        pathPointFinalList.Clear();
        pathPointFinalList = new List<GameObject>();
    }

    private void DeletePath()
    {
        foreach (var point in pathPointDeletionList)
            Destroy(point);
        counter = 0;
    }
}