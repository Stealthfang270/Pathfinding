using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] GameObject currentNode, nextNode, startNode, destinationNode;

    [SerializeField] float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentNode = startNode;
        nextNode = currentNode;

        transform.position = currentNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, nextNode.gameObject.transform.position) < 0.1f)
        {
            currentNode = nextNode;

            nextNode = currentNode.GetComponent<Pathnode>().connections[Random.Range(0, currentNode.GetComponent<Pathnode>().connections.Count)];

        }
        else
        {
            transform.Translate((nextNode.transform.position - transform.position).normalized * movementSpeed * Time.deltaTime);
        }

    }
}
