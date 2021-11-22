using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkingsound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void walkingsound(){
        AkSoundEngine.PostEvent("FOOTSTEPS", gameObject);
        Debug.Log("Foot");
    }
    private void tinyfoot(){
         AkSoundEngine.PostEvent("TinyFootsteps", gameObject);
         Debug.Log("Foot");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
