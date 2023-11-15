using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] GameObject currentNode, nextNode, startNode, destinationNode, previousNode;

    [SerializeField] float movementSpeed;

    public List<GameObject> paths;
    public List<float> costs;

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
        if (currentNode == destinationNode)
        {
            destinationNode = startNode;
            startNode = currentNode;
        }
        else
        {
            if (Vector3.Distance(transform.position, nextNode.transform.position) < 0.1f)
            {
                currentNode = nextNode;
                var nextNodeScript = nextNode.GetComponent<Pathnode>();
                List<GameObject> selections = new List<GameObject>();
                for(int i = 0; i < nextNodeScript.connections.Count; i++)
                {
                    var currentConnectionNode = nextNodeScript.connections[i];
                    if (costs[paths.IndexOf(currentConnectionNode)] < costs[paths.IndexOf(nextNode)])
                    {
                        selections.Add(currentConnectionNode);
                    }
                }
                if (selections.Count > 0)
                {
                    nextNode = selections[Random.Range(0, selections.Count)];
                }
            }
            else
            {
                transform.Translate((nextNode.transform.position - transform.position).normalized * movementSpeed * Time.deltaTime);
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
                if (!paths.Contains(currentPath.connections[j]))
                {
                    paths.Add(currentPath.connections[j]);
                    costs.Add(currentCost + 1);
                }
            }
        }
    }
}
