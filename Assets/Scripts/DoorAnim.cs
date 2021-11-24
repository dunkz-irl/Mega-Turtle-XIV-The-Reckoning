using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    public DoorPlaceholder ParentDoor;

    public void OnFinishAnim()
    {
        ParentDoor.OnAnimFinish();
    }
}
