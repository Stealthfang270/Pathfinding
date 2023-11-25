using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSpy : MonoBehaviour
{
    public GameObject startNode, keyNode, jailDoorNode, disguiseNode, intelligenceDoorNode, intelligenceNode;
    public float disguiseTime, jailDoorTime, intelligenceGrabTime;
    private float disguiseTimeLeft, jailDoorTimeLeft, intelligenceGrabTimeLeft;
    public Pathfinder pathfinder;
    public State state;
    bool keyFound = false;
    bool intelligenceDoorOpened = false;
    public bool disguised = false;
    bool hasIntelligence = false;
    BoxCollider detector;
    public GameObject lockedIntelDoor, lockedJailDoor;
    public Material baseMaterial, disguisedMaterial;
    public GameObject body;
    public Chaser chaser;

    public GameObject key;
    public GameObject intelligence;
    public GameObject intelligenceTransform;

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
        if(disguised && disguiseTimeLeft > 0)
        {
            disguiseTimeLeft -= Time.deltaTime;
            tag = "Disguised";
            chaser.target = null;
        } else if(disguiseTimeLeft <= 0)
        {
            disguised = false;
            disguiseTimeLeft = disguiseTime;
            body.GetComponent<Renderer>().material = baseMaterial;
            tag = "Spy";
        }
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
                    state = State.GrabIntelligence;
                    pathfinder.destinationNode = intelligenceNode;
                    pathfinder.CreatePath();
                }
                break;
            case State.GrabIntelligence:
                if (pathfinder.atDestination && intelligenceGrabTimeLeft > 0)
                {
                    intelligenceGrabTimeLeft -= Time.deltaTime;
                }
                else if (pathfinder.atDestination)
                {
                    state = State.Escape;
                    intelligence.transform.position = intelligenceTransform.transform.position;
                    intelligence.transform.rotation = intelligenceTransform.transform.rotation;
                    intelligence.transform.parent = intelligenceTransform.transform;
                    hasIntelligence = true;
                    pathfinder.destinationNode = startNode;
                    pathfinder.CreatePath();
                }
                break;
            case State.Escape:
                //Yeah nothing really needs to go here lmao
                break;
            case State.Jailed:
                if (pathfinder.atDestination && jailDoorTimeLeft > 0)
                {
                    jailDoorTimeLeft -= Time.deltaTime;
                } else if(pathfinder.atDestination)
                {
                    jailDoorTimeLeft = jailDoorTime;
                    lockedJailDoor.GetComponent<Pathnode>().activeNode = true;
                    ResetStateBasedOnBools();
                }
                break;
            case State.Disguise:
                if(pathfinder.atDestination)
                {
                    disguised = true;
                    body.GetComponent<Renderer>().material = disguisedMaterial;
                    ResetStateBasedOnBools();
                }
                break;
        }
    }

    void ResetStateBasedOnBools()
    {
        if (hasIntelligence)
        {
            state = State.Escape;
            pathfinder.destinationNode = startNode;
            pathfinder.CreatePath();
        }
        else if (intelligenceDoorOpened)
        {
            state = State.GrabIntelligence;
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
        GrabIntelligence,
        Escape,
        Jailed,
        Disguise
    }
}
