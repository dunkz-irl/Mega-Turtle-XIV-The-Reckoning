using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isOccupied;
    public bool hasPlayerAction = false;
    public FollowerController Occupier;
    public DoorPlaceholder ConnectedDoor;
    public Transform FollowPoint;

    private PlayerController pc;

    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && pc.isInPressurePlateTrigger == false)
        {
            pc.selectedPressurePlate = this;
            pc.isInPressurePlateTrigger = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && pc.isInPressurePlateTrigger == true)
        {
            pc.selectedPressurePlate = null;
            pc.isInPressurePlateTrigger = false;
        }
    }
}
