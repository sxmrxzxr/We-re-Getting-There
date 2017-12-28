using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class SimpleBusController : MonoBehaviour
{
    public List<AxleInfo> AxleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    public List<Passenger> currentPassengers;
    public readonly int maxPassengers;

    public void DisembarkAndBoardPassengers(List<Passenger> passengers, StopController stop)
    {
        foreach (Passenger p in currentPassengers.FindAll(p => p.Destination == stop))
        {
            currentPassengers.Remove(p);
        }
        foreach (Passenger p in passengers)
        {
            currentPassengers.Add(p);
        }
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        float brake = Input.GetKey("space") ? 1500 * .5f : 0;

        foreach (AxleInfo axleInfo in AxleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;

                axleInfo.leftWheel.brakeTorque = brake;
                axleInfo.rightWheel.brakeTorque = brake;
            }
            
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);

            // Debug.Log("motor: " + axleInfo.leftWheel.motorTorque + ", " + "input: " + Input.GetAxis("Vertical") + ", " + "braked: " + brake);
        }        
    }
}
