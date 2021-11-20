using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorPlaceholder : MonoBehaviour
{
    public PressurePlate ConnectedPressurePlate;
    public UnityAction OnAnimFinish;
    private GameObject[] pressurePlates;

    // Start is called before the first frame update
    void Start()
    {
        pressurePlates = GameObject.FindGameObjectsWithTag("PressurePlate");
        ConnectedPressurePlate.GetComponentInChildren<PressurePlateInnerCollider>().OnTurtleEnterPressurePlate += checkLockStatus;
    }

    void checkLockStatus()
    {
        Debug.Log("Door has checked conditions");

        // TODO: Animate properly
        if (ConnectedPressurePlate.isOccupied)
        {
            GetComponentInChildren<Animator>().SetTrigger("OpenDoor");
        }
        else // Pressure plate is not occupied
        {
            GetComponentInChildren<Animator>().SetTrigger("CloseDoor");
        }
    }
    
    void animFinish()
    {
        OnAnimFinish();
    }
}
