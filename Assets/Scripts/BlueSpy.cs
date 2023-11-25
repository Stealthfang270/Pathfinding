using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpy : MonoBehaviour
{
    public GameObject startNode, keyNode, jailDoorNode, hideNode, intelligenceDoorNode, intelligenceNode;
    public float hideTime, jailDoorTime, intelligenceGrabTime;
    private float hideTimeLeft, jailDoorTimeLeft, intelligenceGrabTimeLeft;
    public Pathfinder pathfinder;
    public State state;
    bool keyFound = false;
    bool intelligenceDoorOpened = false;
    public bool hidden = false;
    BoxCollider detector;
    public GameObject lockedIntelDoor, lockedJailDoor;
    public GameObject body;
    public GameObject box;
    public Chaser chaser;

    public GameObject key;
    public GameObject intelligence;
    public GameObject intelligenceTransform;

    private void Awake()
    {
        state = State.KeySearch;
        pathfinder.startNode = startNode;
        pathfinder.destinationNode = keyNode;

        hideTimeLeft = hideTime;
        jailDoorTimeLeft = jailDoorTime;
        intelligenceGrabTimeLeft = intelligenceGrabTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.KeySearch:
                if (pathfinder.atDestination)
                {
                    keyFound = true;
                    state = State.Infiltrate;
                    pathfinder.destinationNode = intelligenceDoorNode;
                    pathfinder.CreatePath();
                    key.SetActive(false);
                }
                break;
            case State.Infiltrate:
                if (pathfinder.atDestination)
                {
                    lockedIntelDoor.GetComponent<Pathnode>().activeNode = true;
                    state = State.DestroyIntelligence;
                    pathfinder.destinationNode = intelligenceNode;
                    pathfinder.CreatePath();
                }
                break;
            case State.DestroyIntelligence:
                if (pathfinder.atDestination && intelligenceGrabTimeLeft > 0)
                {
                    intelligenceGrabTimeLeft -= Time.deltaTime;
                }
                else if (pathfinder.atDestination)
                {
                    Destroy(intelligence);
                }
                break;
            case State.Jailed:
                if (pathfinder.atDestination && jailDoorTimeLeft > 0)
                {
                    jailDoorTimeLeft -= Time.deltaTime;
                }
                else if (pathfinder.atDestination)
                {
                    jailDoorTimeLeft = jailDoorTime;
                    lockedJailDoor.GetComponent<Pathnode>().activeNode = true;
                    ResetStateBasedOnBools();
                }
                break;
            case State.Hide:
                if (pathfinder.atDestination && hideTimeLeft > 0)
                {
                    hidden = true;
                    hideTimeLeft -= Time.deltaTime;
                    tag = "Disguised";
                    chaser.target = null;
                    box.SetActive(true);
                } else if(pathfinder.atDestination)
                {
                    hidden = false;
                    hideTimeLeft = hideTime;
                    tag = "Spy";
                    box.SetActive(false);
                    ResetStateBasedOnBools();
                }
                break;
        }
    }

    void ResetStateBasedOnBools()
    {
        if (intelligenceDoorOpened)
        {
            state = State.DestroyIntelligence;
            pathfinder.destinationNode = intelligenceNode;
            pathfinder.CreatePath();
        }
        else if (keyFound)
        {
            state = State.Infiltrate;
            pathfinder.destinationNode = intelligenceDoorNode;
            pathfinder.CreatePath();
        }
        else
        {
            state = State.KeySearch;
            pathfinder.destinationNode = keyNode;
            pathfinder.CreatePath();
        }
    }

    public enum State
    {
        KeySearch,
        Infiltrate,
        DestroyIntelligence,
        Jailed,
        Hide
    }
}
