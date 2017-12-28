using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StopController : MonoBehaviour {

    public bool visited;
    public List<Passenger> passengers;
    public string stopName;

    private float currentTimerValue;
    private float timerValue = 5;
    
    private IEnumerator StartBoardingTimer()
    {
        currentTimerValue = timerValue;

        while (currentTimerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTimerValue--;
        }
    }

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        visited = false;
        passengers.Select(p => { p.Departure = this; return p; }).ToList();
    }

    void OnTriggerEnter(Collider c)
    {
        StartCoroutine(StartBoardingTimer());        
    }

    void OnTriggerStay(Collider c)
    {
        SimpleBusController bus = c.gameObject.GetComponentInParent<SimpleBusController>();

        Debug.Log(currentTimerValue);

        if (currentTimerValue == 0)
        {
            bus.DisembarkAndBoardPassengers(passengers, this);         
            currentTimerValue = -1;
            visited = true;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void OnTriggerExit(Collider c)
    {
        
    }
}
