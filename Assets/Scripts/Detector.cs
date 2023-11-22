using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Detector : MonoBehaviour
{
    public Chaser chaser;
    public RedSpy redSpy;
    private void OnTriggerEnter(Collider other)
    {
        if (chaser != null)
        {
            if (other.gameObject.tag == "Spy")
            {
                if (chaser.patroller != null)
                {
                    chaser.patroller.enabled = false;
                }
                chaser.target = other.gameObject;
            }
        }

        if(redSpy != null)
        {
            if(other.gameObject.tag == "Guard" && !redSpy.disguised)
            {
                redSpy.state = RedSpy.State.Disguise;
                redSpy.pathfinder.destinationNode = redSpy.disguiseNode;
                redSpy.pathfinder.CreatePath();
                redSpy.chaser = other.gameObject.transform.root.GetComponent<Chaser>();
            }
        }
    }
}
