using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RouteController : MonoBehaviour {

    public GameObject StartNode;
    public List<StopController> Stops;
    public EndNodeController EndNode;

	// Use this for initialization
	void Start () {
		
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
