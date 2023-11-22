using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSpy : MonoBehaviour
{
    public GameObject startNode, keyNode, jailDoorNode, disguiseNode, intelligenceDoorNode, intelligenceNode;
    public float disguiseTime, jailDoorTime, intelligenceGrabTime;
    private float disguiseTimeLeft, jailDoorTimeLeft, intelligenceGrabTimeLeft;
    public Pathfinder pathfinder;
    State state;
    bool keyFound = false;
    bool disguised = false;
    bool hasIntelligence = false;
    BoxCollider detector;
    public GameObject lockedIntelDoor, lockedJailDoor;

    private void Awake()
    {
        state = State.KeySearch;
        pathfinder.startNode = startNode;
        pathfinder.destinationNode = keyNode;

        disguiseTimeLeft = disguiseTime;
        jailDoorTimeLeft = jailDoorTime;
        intelligenceGrabTimeLeft = intelligenceGrabTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.KeySearch:
                if(pathfinder.atDestination)
                {
                    keyFound = true;
                    state = State.Infiltrate;
                    pathfinder.destinationNode = intelligenceDoorNode;
                    pathfinder.CreatePath();
                }
                break;
            case State.Infiltrate:
                if(pathfinder.atDestination)
                {
                    lockedIntelDoor.GetComponent<Pathnode>().activeNode = true;
                    state = State.GrabIntelligence;
                    pathfinder.destinationNode = intelligenceNode;
                    pathfinder.CreatePath();
                }
                break;
            case State.GrabIntelligence:
                if(pathfinder.atDestination && intelligenceGrabTimeLeft > 0)
                {
                    intelligenceGrabTimeLeft -= Time.deltaTime;
                }
                else if(pathfinder.atDestination) 
                {
                    state = State.Escape;
                    hasIntelligence = true;
                    pathfinder.destinationNode = startNode;
                    pathfinder.CreatePath();
                }
                break;
            case State.Escape:
                //Yeah nothing really needs to go here lmao
                break;
            case State.Jailed:
                break;
            case State.Disguise:
                break;
        }
    }

    public enum State
    {
        KeySearch,
        Infiltrate,
        GrabIntelligence,
        Escape,
        Jailed,
        Disguise
    }
}
