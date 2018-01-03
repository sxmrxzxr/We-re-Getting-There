using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNodeController : MonoBehaviour {

    public bool visited;

    void Start()
    {
        visited = false;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.name == "Cube")
        {
            visited = true;
        }
    }

}
