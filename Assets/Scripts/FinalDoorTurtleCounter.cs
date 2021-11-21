using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorTurtleCounter : MonoBehaviour
{
    int turtlecounter;
    public PlayerController player;
    private void Start()
    {
        turtlecounter = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Follower")
        {
            turtlecounter++;
            if (turtlecounter==3)
                player.TurtlesArrived();
        }   
    }
}
