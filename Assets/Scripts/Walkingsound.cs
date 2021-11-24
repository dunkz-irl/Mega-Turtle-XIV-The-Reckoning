using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkingsound : MonoBehaviour
{
    private void walkingsound(){
        AkSoundEngine.PostEvent("FOOTSTEPS", gameObject);
    }
    private void tinyfoot(){
         AkSoundEngine.PostEvent("TinyFootsteps", gameObject);
    }
}
