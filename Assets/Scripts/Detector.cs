using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Detector : MonoBehaviour
{
    public Chaser chaser;
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


    }
}
