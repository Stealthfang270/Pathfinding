using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorMenuPathnodes : MonoBehaviour
{
    static GameObject node;
    static GameObject[] spawner;
    static GameObject[] allSpheres;

    static Transform selectedObject;

    static RaycastHit hit;
    static RaycastHit hit2;

    [MenuItem("Grid Generation/Spawn Nodes", priority = 0)]

    static void SpawnNodes()
    {        
        Vector3 floorExtents;

        node = Resources.Load("Pathnode") as GameObject;
        
        selectedObject = Selection.transforms[0];
        floorExtents = selectedObject.GetComponent<BoxCollider>().bounds.extents - new Vector3(2, 0, 2);
        Debug.Log(floorExtents);
        for (float i = floorExtents.x * -2; i <= 0; i += 2)
        {
            for (float j = floorExtents.z * -2; j <= 0; j += 2)
            {
                if (Physics.SphereCast(selectedObject.position + floorExtents + selectedObject.right * i + selectedObject.forward * j, 0.5f, Vector3.down, out hit))
                {
                    if (Physics.Raycast(selectedObject.position + floorExtents + selectedObject.right * i + selectedObject.forward * j, Vector3.down, out hit2))
                    {
                        if (hit.transform == hit2.transform)
                        {
                            GameObject spawnedNode = Instantiate(node, hit.point, Quaternion.identity);
                            spawnedNode.transform.parent = selectedObject;
                        }
                    }
                }
            }
        }
    }


    [MenuItem("Grid Generation/Clear Trigger Nodes", priority = 0)]

    static void ClearTriggerNodes()
    {
        selectedObject = Selection.transforms[0];

        for (int i = 0; i < selectedObject.childCount;)
        {
            DestroyImmediate(selectedObject.GetChild(0).gameObject);
        }
    }

    [MenuItem("Grid Generation/Clear All Nodes", priority = 0)]

    static void ClearAllNodes()
    {
        allSpheres = GameObject.FindGameObjectsWithTag("Pathnode");

        foreach (GameObject target in allSpheres)
        {
            DestroyImmediate(target);
        }
    }

    [MenuItem("Spherical/Change Color/Red", priority = 0)]

    static void ChangeToRed()
    {
        foreach (Transform sphere in Selection.transforms)
        {
            sphere.GetComponent<Renderer>().material = Resources.Load("RedMAT") as Material;
        }
    }

    [MenuItem("Spherical/Change Color/Red", true)]
    
    static bool ChangeToRedValidate()
    {
        bool valid = true;

        foreach (Transform sphere in Selection.transforms)
        {
            Debug.Log("Loop");
            if (sphere.tag != "Sphere")
            {
                valid = false;
                break;
            }
        }
        return valid;
    }

    [MenuItem("Spherical/Change Color/Blue", priority = 0)]

    static void ChangeToBlue()
    {
        foreach (Transform sphere in Selection.transforms)
        {
            sphere.GetComponent<Renderer>().material = Resources.Load("BlueMAT") as Material;
        }
    }

    //[MenuItem("Spherical/Change Color/Blue", true)]

    //static bool ChangeToBlueValidate()
    //{
    //    bool valid = true;

    //    foreach (Transform sphere in Selection.transforms)
    //    {
    //        Debug.Log("Loop");
    //        if (sphere.tag != "Sphere")
    //        {
    //            valid = false;
    //            break;
    //        }
    //    }
    //    return valid;
    //}
}
