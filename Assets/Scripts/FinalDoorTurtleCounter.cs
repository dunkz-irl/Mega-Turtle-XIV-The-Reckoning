using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorTurtleCounter : MonoBehaviour
{
    static int turtlecounter;
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
            if (turtlecounter==4)
                player.TurtlesArrived();
        }   
    }
}
