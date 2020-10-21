using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTeleportObject : MonoBehaviour
{

   
    public List<ObjectReset> objectsToReset = new List<ObjectReset>();

    private bool EditModeActive = false;
    private void OnEnable()
    {
        GameEvents.OnEditActive += () => { EditModeActive = true; };
        GameEvents.OnEditDeActive += () => { EditModeActive = false; };
    }

    private void OnDisable()
    {
        GameEvents.OnEditActive -= () => { EditModeActive = true; };
        GameEvents.OnEditDeActive -= () => { EditModeActive = false; };
    }

    public void ResetObjects()
    {
        if(EditModeActive)
        {
            return;
        }

        for (int i = 0; i < objectsToReset.Count; i++)
        {
            objectsToReset[i].Reset();
        }
    }

}

[System.Serializable]
public class ObjectReset
{
    public Transform objectToMove;
    public Transform objectToMoveTo;
    public bool matchRotation = true;


    public void Reset()
    {
        if (objectToMove == null || objectToMoveTo == null)
        {
            return;
        }
        else
        {
            Debug.Log("There is no transform to move or to move to.");
        }

        objectToMove.transform.position = objectToMoveTo.transform.position;
        if(matchRotation)
        {
            objectToMove.transform.rotation = objectToMoveTo.transform.rotation;
        }

        if(objectToMove.GetComponent<Rigidbody>())
        {
            objectToMove.GetComponent<Rigidbody>().velocity = Vector3.zero;
            objectToMove.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

}
