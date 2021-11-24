using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorPlaceholder : MonoBehaviour
{
    public PressurePlate ConnectedPressurePlate;
    public UnityAction OnAnimFinish;

    // Start is called before the first frame update
    void Start()
    {
        //ConnectedPressurePlate.GetComponentInChildren<PressurePlateInnerCollider>().OnTurtleEnterPressurePlate += checkLockStatus;
    }

    public void checkLockStatus(bool val)
    {
        if (val)
        {
            GetComponentInChildren<Animator>().SetTrigger("OpenDoor");
            AkSoundEngine.PostEvent("DOOR", gameObject);
        }
        else // Pressure plate is not occupied
        {
            GetComponentInChildren<Animator>().SetTrigger("CloseDoor");
            AkSoundEngine.PostEvent("DOOR", gameObject);
        }
    }
}
