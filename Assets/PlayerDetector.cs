using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    FollowerController fc;

    private void Start()
    {
        fc = GetComponentInParent<FollowerController>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !fc.isFollowing)
        {
            fc.InitFollow();
        }
    }
}
