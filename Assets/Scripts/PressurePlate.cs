using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool isOccupied;
    public bool IsOccupied
    {
        get => isOccupied;
        set
        {
            foreach (var door in ConnectedDoors)
            {
                door.checkLockStatus(value);
            }            
            isOccupied = value;

            // Music layers
            if (value == false)
                MusicLayerMuteEvent.Post(pc.gameObject);
            else if (value == true)
                MusicLayerUnmuteEvent.Post(pc.gameObject);
        }
            
    }
    public bool hasPlayerAction = false;
    public FollowerController Occupier;
    public DoorPlaceholder[] ConnectedDoors;
    public Transform FollowPoint;
    public AK.Wwise.Event MusicLayerMuteEvent;
    public AK.Wwise.Event MusicLayerUnmuteEvent;

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
