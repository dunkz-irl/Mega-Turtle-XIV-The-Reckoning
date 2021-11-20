using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateInnerCollider : MonoBehaviour
{
    public UnityAction OnTurtleEnterPressurePlate;
    public PressurePlate ParentPressurePlate;

    private SphereCollider sphCol;

    private void Start()
    {
        sphCol = GetComponent<SphereCollider>();
        //ParentPressurePlate.ConnectedDoor.OnAnimFinish += finishTurtleOccupation;
    }

    private void finishTurtleOccupation()
    {
        sphCol.enabled = true;
        ParentPressurePlate.hasPlayerAction = false;        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Follower" && !ParentPressurePlate.IsOccupied)
    //    {            
    //        sphCol.enabled = false;
    //        OnTurtleEnterPressurePlate();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Follower")
    //    {            
    //        sphCol.enabled = false;
    //        OnTurtleEnterPressurePlate();
    //    }
    //}
}
