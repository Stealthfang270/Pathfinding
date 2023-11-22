using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public Patroller patroller;
    public Pathfinder pathfinder;
    public GameObject target;
    public float regularSpeed;
    public float chaseSpeed;

    private void Update()
    {
        if (target != null)
        {
            if (patroller != null) { patroller.enabled = false; }
            if (pathfinder.destinationNode != target.transform.root.GetComponent<Pathfinder>().nextNode)
            {
                pathfinder.destinationNode = target.transform.root.GetComponent<Pathfinder>().nextNode;
                pathfinder.CreatePath();
                pathfinder.movementSpeed = chaseSpeed;
            }
        } else
        {
            pathfinder.movementSpeed = regularSpeed;
            if(patroller != null) { patroller.enabled = true; }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spy")
        {
            Destroy(collision.gameObject);
        }
    }
}
