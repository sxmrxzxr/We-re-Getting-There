using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public SimpleBusController bus;
    public RouteController route;
    public GameManager game;

    public Text passengersText;
    public Text nextStopText;
    public Text timerText;
    public Text gameResultText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        passengersText.text = "Passengers: " + bus.currentPassengers.Count;
        nextStopText.text = "Next Stop: " + route.GetNextStop();
        timerText.text = game.GetCurrentTimerValue();
        gameResultText.text = game.GetGameResult();
	}
}
