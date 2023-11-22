using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSpy : MonoBehaviour
{
    public GameObject keyNode, jailDoorNode, disguiseNode, intelligenceDoorNode, intelligenceNode;
    public Pathfinder pathfinder;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum State
    {
        KeySearch,
        Infiltrate,
        GrabIntelligence,
        Escape,
        Jailed,
        Disguise,
        Disguised
    }
}
