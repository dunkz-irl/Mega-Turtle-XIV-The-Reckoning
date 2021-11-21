using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownPressurePlate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {     
            // For opening all doors
            //
            //PressurePlate[] pressureplates = FindObjectsOfType<PressurePlate>();
            //foreach (var pressurePlate in pressureplates)
            //{
            //    pressurePlate.IsOccupied = true;
            //}

            // Manage follower navigation to final point
            PlayerController player = other.GetComponent<PlayerController>();
            foreach (var follower in player.MoheyFollowers)
            {
                follower.isAwake = false;
                follower.SetMovementTarget(null, 1f);
                follower.GetComponentInChildren<Animator>().SetBool("isHiding", false);
                player.StepOnFinalButton();
            }

            // Open Brown doors only
            player.selectedPressurePlate.IsOccupied = true;
        }
    }
}
