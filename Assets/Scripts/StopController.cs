using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StopController : MonoBehaviour {

    // Stop Logic
    public bool visited;
    public List<Passenger> passengers;
    public string stopName;

    private float currentTimerValue;
    private float timerValue = 5;

    // For Spawning Models
    public GameObject passenger;
    public Transform passengerArea;
    public Transform disembarkArea;
    
    private IEnumerator StartBoardingTimer()
    {
        currentTimerValue = timerValue;

        while (currentTimerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTimerValue--;
            Debug.Log(currentTimerValue);
        }
    }

    void FirstRenderPassengers()
    {
        foreach (Passenger p in passengers)
        {
            Vector3 pos = new Vector3(passengerArea.position.x, .25f, passengerArea.position.z);
            GameObject newPassenger = Instantiate(passenger, pos, Quaternion.identity);
            newPassenger.transform.parent = passengerArea;
        }
    }

    void RenderDisembarkingPassengers(List<Passenger> disembarkingPassengers)
    {
        Debug.Log(disembarkingPassengers.FindAll(d => d.Destination.stopName == this.stopName));
        foreach (Passenger p in disembarkingPassengers.FindAll(d => d.Destination.stopName == this.stopName))
        {
            //Debug.Log(p);
            Vector3 pos = new Vector3(disembarkArea.position.x, .25f, disembarkArea.position.z);
            GameObject newPassenger = Instantiate(passenger, pos, Quaternion.identity);
            newPassenger.transform.parent = disembarkArea;
        }
    }

    void DestroyPassengers()
    {
        foreach (Transform g in passengerArea.GetComponentsInChildren<Transform>())
        {
            //Debug.Log(g);
            if (g.tag == "Passenger")
            {
                Debug.Log("g is passenger");
                Destroy(g.gameObject);
            }
        }
    }

    void Start()
    {
        visited = false;
        passengers.Select(p => { p.Departure = this; return p; }).ToList();

        FirstRenderPassengers();       
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log(c);

        if (c.name == "Cube")
            StartCoroutine(StartBoardingTimer());        
    }

    void OnTriggerStay(Collider c)
    {
        SimpleBusController bus = c.gameObject.GetComponentInParent<SimpleBusController>();

        if (currentTimerValue == 0)
        {
            // Debug.Log("got here");

            // currentTimerValue = -1;
            visited = true;            
            
            bus.BoardPassengers(passengers);
            passengers.RemoveRange(0, passengers.Count);

            RenderDisembarkingPassengers(bus.currentPassengers);
            bus.DisembarkPassengers(this);

            DestroyPassengers();
        }
    }

    void OnTriggerExit(Collider c)
    {
        
    }
}
