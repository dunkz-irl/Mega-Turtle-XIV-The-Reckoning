using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlaceholder : MonoBehaviour
{
    private GameObject[] pressurePlates;

    void checkLockStatus()
    {
        int count = 0;

        foreach (var pressurePlate in pressurePlates)
        {
            if (pressurePlate.GetComponent<PressurePlate>().isOccupied)
            {
                count++;
            }
        }

        if (count == 2)
        {
            GetComponent<Animator>().SetTrigger("OpenDoor");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pressurePlates = GameObject.FindGameObjectsWithTag("PressurePlate");
        PressurePlate.OnPlayerEnterPressurePlate += checkLockStatus;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
