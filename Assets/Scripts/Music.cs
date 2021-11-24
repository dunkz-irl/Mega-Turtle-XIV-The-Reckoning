using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
     private static GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
         DontDestroyOnLoad(gameObject);
     if (instance == null){
         instance = gameObject;
          AkSoundEngine.PostEvent("PLAYMUSIC", gameObject);
     }
     else{
         Destroy(gameObject);
     }
    }
}
