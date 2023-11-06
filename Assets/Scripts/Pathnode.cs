using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathnode : MonoBehaviour
{
    public List<Pathnode> paths = new List<Pathnode>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }

    public void BuildPaths()
    {

    }

    public void ClearPaths()
    {

    }
}
