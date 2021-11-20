using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateInnerCollider : MonoBehaviour
{
    public UnityAction OnTurtleEnterPressurePlate;

    private SphereCollider sphCol;
    private PressurePlate pressurePlate;

    private void Start()
    {
        sphCol = GetComponent<SphereCollider>();
        GetComponentInParent<PressurePlate>().ConnectedDoor.OnAnimFinish += reenableSphereCollider;
        pressurePlate = GetComponentInParent<PressurePlate>();
    }

    private void reenableSphereCollider()
    {
        sphCol.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Follower" && !pressurePlate.isOccupied)
        {            
            sphCol.enabled = false;
            pressurePlate.isOccupied = true;
            OnTurtleEnterPressurePlate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Follower" && pressurePlate.isOccupied)
        {            
            sphCol.enabled = false;
            pressurePlate.isOccupied = false;
            OnTurtleEnterPressurePlate();
        }
    }
}
