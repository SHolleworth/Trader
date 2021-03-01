using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GalaxyMarker : MonoBehaviour
{
    bool playerLocation = false;
    bool reachable = false;
    [SerializeField] int id = -1;
    [SerializeField] SelectionMarker sm = null;
    [SerializeField] Color smBaseColor = Color.red;
    [SerializeField] bool activated = false;
    [SerializeField] GalaxyMarker[] neighbours = null;

    [SerializeField] GameObject pathPoint = null;
    List<GameObject> pathPointDeletionList;
    //Linear Bezier points for drawing a curved path
    Vector3 point0;
    Vector3 point1;
    //t Bezier value
    float x;
    //distance between origin and destination
    float distance;
    //control the amount of points in a path
    float counter;
    float counterIncrement;
    float counterAdjustment = 1;

    public bool Reachable { get { return reachable; } set { reachable = value; } }

    private void Awake()
    {
        pathPointDeletionList = new List<GameObject>();
        reachable = false;
    }

    void Start()
    {
        Debug.Log(id);
        sm.SetColor(smBaseColor);
        DrawRoutes();
        if (PlayerPrefs.GetInt(StatHandler.idName) == id)
        {
            Debug.Log(name);
            playerLocation = true;
            reachable = true;
            foreach (GalaxyMarker neighbour in neighbours)
                neighbour.Reachable = true;
        }
    }

    void OnMouseOver()
    {
        if(activated)
            sm.Show();
    }

    void OnMouseExit()
    {
        sm.Hide();
    }

    void OnMouseDown()
    {
        if (reachable)
            SceneManager.LoadScene("01System" + id);
    }

    void DrawRoutes()
    {
        foreach(GalaxyMarker neighbour in neighbours)
            DrawPath(neighbour.transform.position);
    }

    void FindDistance(Vector3 destination)
    {
        distance = Vector3.Distance(transform.position, destination);
        counterIncrement = (1 / distance) / counterAdjustment;
    }

    private void DrawPath(Vector3 destination)
    {
        counter = 0;
        point0 = transform.position;
        point1 = destination;
        FindDistance(destination);
        Vector3 pointAlongTheLine;
        pathPoint.GetComponent<PathPoint>().EnableSpriteRenderer();
        pathPoint.GetComponent<CircleCollider2D>().enabled = false;
        GameObject newPoint;
        while (true)
        {
            //draw point
            newPoint = CalculateAndDrawNextPoint();
            pointAlongTheLine = newPoint.transform.position;
            if (Vector3.Distance(pointAlongTheLine, destination) < 0.001f)
            {
                break;
            }
        }
    }

    GameObject CalculateAndDrawNextPoint()
    {
        counter += counterIncrement;
        x = Mathf.Lerp(0, 1, counter);

        Vector3 pointAlongTheLine = LinearBezierPoint(x, point0, point1);

        GameObject newPoint = Instantiate(pathPoint, pointAlongTheLine, Quaternion.identity);

        pathPointDeletionList.Add(newPoint);

        return newPoint;
    }

    Vector3 LinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return (1 - t) * p0 + t * p1;
    }
}
