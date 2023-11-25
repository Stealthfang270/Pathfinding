using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Pathnode node;
    public float openRot;
    public float closeRot;

    private void Update()
    {
        if(node.activeNode)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, openRot, 0));
        } else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, closeRot, 0));
        }
    }
}
