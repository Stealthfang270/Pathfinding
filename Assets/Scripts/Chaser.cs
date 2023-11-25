using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public Patroller patroller;
    public Pathfinder pathfinder;
    public GameObject target;
    public GameObject jailNode, jailDoorNode;
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
            jailDoorNode.GetComponent<Pathnode>().activeNode = false;
            var parent = collision.gameObject.transform.root;
            var spyPathfinder = parent.GetComponent<Pathfinder>();
            var redSpy = parent.GetComponent<RedSpy>();
            var blueSpy = parent.GetComponent<BlueSpy>();

            //Set position of spy
            spyPathfinder.startNode = jailNode;
            spyPathfinder.currentNode = jailNode;
            spyPathfinder.nextNode = jailNode;
            parent.position = new Vector3(jailNode.transform.position.x, parent.position.y, jailNode.transform.position.z);
            
            if (redSpy != null)
            {
                redSpy.state = RedSpy.State.Jailed;
                spyPathfinder.destinationNode = redSpy.jailDoorNode;
                spyPathfinder.CreatePath();
            }

            if(blueSpy != null)
            {
                blueSpy.state = BlueSpy.State.Jailed;
                spyPathfinder.destinationNode = blueSpy.jailDoorNode;
                spyPathfinder.CreatePath();
            }
            
            target = null;
        }
    }
}
