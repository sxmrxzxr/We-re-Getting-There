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

    private bool arePassengersDisembarked;
    
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
            Vector3 pos = new Vector3(Random.Range(passengerArea.position.x, passengerArea.position.x - 1.25f), 0.25f, Random.Range(passengerArea.position.z, passengerArea.position.z + 1.5f));
            GameObject newPassenger = Instantiate(passenger, pos, Quaternion.identity);
            newPassenger.transform.parent = passengerArea;
        }
    }

    private IEnumerator RenderDisembarkingPassengers(List<Passenger> disembarkingPassengers)
    {
        foreach (Passenger p in disembarkingPassengers.FindAll(d => d.Destination.stopName == this.stopName))
        {
            yield return new WaitForSeconds(1.0f);
            Vector3 pos = new Vector3(Random.Range(disembarkArea.position.x, disembarkArea.position.x - 1.25f), .25f, Random.Range(disembarkArea.position.z, disembarkArea.position.z + 1.5f));
            GameObject newPassenger = Instantiate(passenger, pos, Quaternion.identity);
            newPassenger.transform.parent = disembarkArea;
        }

        arePassengersDisembarked = true;
    }

    private IEnumerator DestroyPassengers()
    {
        foreach (Transform g in passengerArea.GetComponentsInChildren<Transform>())
        {
            yield return new WaitForSeconds(1.0f);
            if (g != null && g.tag == "Passenger")
            {                
                Destroy(g.gameObject);
            }
        }

        visited = true;
    }

    void Start()
    {
        visited = false;
        passengers.Select(p => { p.Departure = this; return p; }).ToList();

        FirstRenderPassengers();       
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.name == "Cube")
        {
            SimpleBusController bus = c.gameObject.GetComponentInParent<SimpleBusController>();
            StartCoroutine(RenderDisembarkingPassengers(bus.currentPassengers));
            bus.DisembarkPassengers(this);
        }            
    }

    void OnTriggerStay(Collider c)
    {
        SimpleBusController bus = c.gameObject.GetComponentInParent<SimpleBusController>();

        if (arePassengersDisembarked)
        {
            bus.BoardPassengers(passengers);
            passengers.RemoveRange(0, passengers.Count);
            StartCoroutine(DestroyPassengers());
        }
    }

    void OnTriggerExit(Collider c)
    {
        
    }
}
