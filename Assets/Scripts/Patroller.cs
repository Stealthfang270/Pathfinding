using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public List<GameObject> patrolPoints;
    public Pathfinder pathfinder;
    public int currentDestNode = 1;

    private void Awake()
    {
        patrolPoints.RemoveAll(x => x.gameObject.GetComponent<Pathnode>().activeNode == false);
        pathfinder.startNode = patrolPoints[0];
        pathfinder.destinationNode = patrolPoints[1];
    }

    // Update is called once per frame
    void Update()
    {
        patrolPoints.RemoveAll(x => x.gameObject.GetComponent<Pathnode>().activeNode == false);
        if (pathfinder.paths.Contains(pathfinder.startNode))
        {
            patrolPoints.RemoveAll(x => !pathfinder.paths.Contains(x.gameObject));
        }
        if (pathfinder.atDestination)
        {
            currentDestNode++;
            if(currentDestNode >= patrolPoints.Count)
            {
                currentDestNode = 0;
            }
            pathfinder.destinationNode = patrolPoints[currentDestNode];
            pathfinder.CreatePath();
        }
    }
}
