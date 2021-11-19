using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlaceholder : MonoBehaviour
{
    public PressurePlate ConnectedPressurePlate;
    private GameObject[] pressurePlates;

    // Start is called before the first frame update
    void Start()
    {
        pressurePlates = GameObject.FindGameObjectsWithTag("PressurePlate");
        ConnectedPressurePlate.GetComponentInChildren<PressurePlateInnerCollider>().OnTurtleEnterPressurePlate += checkLockStatus;
    }

    void checkLockStatus()
    {
        //int count = 0;

        //foreach (var pressurePlate in pressurePlates)
        //{
        //    if (pressurePlate.GetComponent<PressurePlate>().isOccupied)
        //    {
        //        count++;
        //    }
        //}

        //if (count == 2)
        //{
        //    GetComponent<Animator>().SetTrigger("OpenDoor");
        //}

        Debug.Log("Door has checked conditions");

        // TODO: Animate properly
        if (ConnectedPressurePlate.isOccupied)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else // Pressure plate is not occupied
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }    
}
