using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public GameObject currentNode, nextNode, startNode, destinationNode;

    public float movementSpeed;

    public List<GameObject> paths;
    public List<float> costs;
    public bool atDestination = false;

    // Start is called before the first frame update
    void Start()
    {
        currentNode = startNode;
        nextNode = currentNode;

        transform.position = currentNode.transform.position;
        CreatePath();
    }

    // Update is called once per frame
    void Update()
    {
        //If there's ever an inactive node in the path, regenerate the path
        if(paths.Find(x => x.gameObject.GetComponent<Pathnode>().activeNode == false))
        {
            CreatePath();
        }

        if(!paths.Contains(startNode))
        {
            destinationNode = nextNode;
            CreatePath();
        }

        if (currentNode == destinationNode)
        {
            if (Vector3.Distance(transform.position, destinationNode.transform.position) < 0.1f)
            {
                startNode = currentNode;
                atDestination = true;
            }
        }
        else
        {
            atDestination = false;
            if (Vector3.Distance(transform.position, nextNode.transform.position) < 0.1f)
            {
                currentNode = nextNode;
                Pathnode nextNodeScript = nextNode.GetComponent<Pathnode>();
                List<GameObject> selections = new List<GameObject>();
                float lowestCost = Mathf.Infinity;

                for (int i = 0; i < nextNodeScript.connections.Count; i++)
                {
                    GameObject currentConnectionNode = nextNodeScript.connections[i];
                    if (currentConnectionNode.GetComponent<Pathnode>().activeNode && paths.Contains(currentConnectionNode))
                    {
                        float currentConnectionNodeCost = costs[paths.IndexOf(currentConnectionNode)];
                        float nextNodeCost = costs[paths.IndexOf(nextNode)];
                        //Check if the cost is less than the current node's cost
                        //if it is, check if it's less than the lowest recorded cost
                        //if not, check if it's equal to the lowest recorded cost
                        if (currentConnectionNodeCost < nextNodeCost)
                        {
                            if (currentConnectionNodeCost < lowestCost)
                            {
                                selections.Clear();
                                lowestCost = currentConnectionNodeCost;
                                selections.Add(currentConnectionNode);
                            }
                            else if (currentConnectionNodeCost == lowestCost)
                            {
                                selections.Add(currentConnectionNode);
                            }
                        }
                    }
                }
                if (selections.Count > 0)
                {
                    nextNode = selections[UnityEngine.Random.Range(0, selections.Count)];
                }
            }
            else if(nextNode.GetComponent<Pathnode>())
            {
                transform.LookAt(new Vector3(nextNode.transform.position.x, transform.position.y, nextNode.transform.position.z));
                transform.Translate((nextNode.transform.position - transform.position).normalized * movementSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    public void CreatePath()
    {
        paths.Clear();
        costs.Clear();

        paths.Add(destinationNode);
        costs.Add(1);

        for(int i = 0; i < paths.Count; i++)
        {
            float currentCost = costs[i];
            Pathnode currentPath = paths[i].GetComponent<Pathnode>();
            for (int j = 0; j < currentPath.connections.Count; j++)
            {
                //Check if the node is active first. Ignore if not.
                if (currentPath.connections[j].GetComponent<Pathnode>().activeNode)
                {
                    if (!paths.Contains(currentPath.connections[j]))
                    {
                        paths.Add(currentPath.connections[j]);
                        costs.Add(currentCost + 1);
                    }
                    else if (paths.Contains(currentPath.connections[j]) && costs[paths.IndexOf(currentPath.connections[j])] > costs[i] + 1)
                    {
                        costs[i] = currentCost + 1;
                    }
                }
            }
        }
    }
}
