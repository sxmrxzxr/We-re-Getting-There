using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RouteController : MonoBehaviour {

    public List<StopController> Stops;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsRouteComplete()
    {
        return Stops.FindAll(stop => stop.visited == true).Count == Stops.Count;
    }

    public string GetNextStop()
    {
        return IsRouteComplete() ? "!!!" : Stops.First(stop => stop.visited == false).stopName; // just go with it
    }
}
