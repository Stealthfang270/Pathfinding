using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Detector : MonoBehaviour
{
    public Chaser chaser;
    public RedSpy redSpy;
    public BlueSpy blueSpy;
    private void OnTriggerEnter(Collider other)
    {
        if (chaser != null)
        {
            if (other.gameObject.tag == "Spy")
            {
                if (other.gameObject.transform.root.GetComponent<RedSpy>() != null && other.gameObject.transform.root.GetComponent<RedSpy>().state != RedSpy.State.Jailed)
                {
                    if (chaser.patroller != null)
                    {
                        chaser.patroller.enabled = false;
                    }
                    chaser.target = other.gameObject;
                }

                if (other.gameObject.transform.root.GetComponent<BlueSpy>() != null && other.gameObject.transform.root.GetComponent<BlueSpy>().state != BlueSpy.State.Jailed)
                {
                    if (chaser.patroller != null)
                    {
                        chaser.patroller.enabled = false;
                    }
                    chaser.target = other.gameObject;
                }
            }
        }

        if(redSpy != null)
        {
            if(other.gameObject.tag == "Guard" && !redSpy.disguised && redSpy.state != RedSpy.State.Jailed)
            {
                redSpy.state = RedSpy.State.Disguise;
                redSpy.pathfinder.destinationNode = redSpy.disguiseNode;
                redSpy.pathfinder.CreatePath();
                redSpy.chaser = other.gameObject.transform.root.GetComponent<Chaser>();
            }
        }

        if(blueSpy != null)
        {
            if(other.gameObject.tag == "Guard" && !blueSpy.hidden && blueSpy.state != BlueSpy.State.Jailed)
            {
                blueSpy.state = BlueSpy.State.Hide;
                blueSpy.pathfinder.destinationNode = blueSpy.hideNode;
                blueSpy.pathfinder.CreatePath();
                blueSpy.chaser = other.gameObject.transform.root.GetComponent<Chaser>();
            }
        }
    }
}
