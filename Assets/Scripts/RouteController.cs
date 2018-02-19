using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RouteController : MonoBehaviour {

    public GameObject StartNode;
    public List<StopController> Stops;
    public EndNodeController EndNode;

    private LineRenderer lineRender;

    private void DrawRouteLine()
    {
        lineRender = GetComponent<LineRenderer>();
        
        var points = new Vector3[Stops.Count + 2];
        points[0] = StartNode.transform.position;

        for (int i = 0; i < Stops.Count; i++)
        {
            points[i + 1] = Stops[i].transform.position;
        }

        points[Stops.Count + 1] = EndNode.transform.position;

        lineRender.startWidth = .45f;
        lineRender.endWidth = .45f;

        lineRender.positionCount = points.Length;
        lineRender.SetPositions(points);
    }

	// Use this for initialization
	void Start () {
        DrawRouteLine();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsRouteComplete()
    {
        return Stops.FindAll(stop => stop.visited == true).Count == Stops.Count && EndNode.visited;
    }

    public string GetNextStop()
    {
        if (IsRouteComplete())
        {
            return "!!!";
        }
        else
        {
            if (Stops.FindAll(stop => stop.visited == true).Count == Stops.Count)
            {
                return "Finish the route!";
            }
            else
            {
                return Stops.First(stop => stop.visited == false).stopName;
            }
        }
    }
}
