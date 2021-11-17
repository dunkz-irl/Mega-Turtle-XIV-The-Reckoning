using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public bool isOccupied;
    public FollowerController Occupier;

    public static UnityAction OnPlayerEnterPressurePlate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().selectedPressurePlate = this;
            OnPlayerEnterPressurePlate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().selectedPressurePlate = null;
            OnPlayerEnterPressurePlate();
        }
    }
}
