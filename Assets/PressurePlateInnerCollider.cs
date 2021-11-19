using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateInnerCollider : MonoBehaviour
{
    public UnityAction OnTurtleEnterPressurePlate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Follower")
        {
            OnTurtleEnterPressurePlate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Follower")
        {
            OnTurtleEnterPressurePlate();
        }
    }
}
