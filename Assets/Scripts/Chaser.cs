using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public Patroller patroller;
    public Pathfinder pathfinder;
    public GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (target.tag == "Spy")
        {
            if (patroller != null)
            {
                patroller.enabled = false;

                target = other.gameObject;
            }
        }
    }

    private void Update()
    {
        if (target != null)
        {
            if (patroller != null) { patroller.enabled = false; }
            if (pathfinder.destinationNode != target.GetComponent<Pathfinder>().currentNode)
            {
                pathfinder.destinationNode = target.GetComponent<Pathfinder>().currentNode;
                pathfinder.CreatePath();
            }
        }
    }
}
