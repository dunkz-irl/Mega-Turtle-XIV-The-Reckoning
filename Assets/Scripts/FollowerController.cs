using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : Turtle
{
    public bool isFollowing = false;
    public int FollowerID;

    private Vector3 turtleToTargetVector;
    private bool hasTarget;
    private GameObject playerGO;
    [SerializeField] private Transform targetTransform;

    private void Start()
    {
        InitTurtle();    
    }

    void InitFollow()
    {
        isFollowing = true;
        PlayerController.followers.Add(this); //  Add this turtle follower to the list of followers on the PlayerController script
        FollowerID = PlayerController.followers.Count;

        Debug.Log("Follow Initialised, isFollowing is " + isFollowing + ", ID is " + PlayerController.followers.Count);
    }
    
    // Set horizontalInput and verticalInput to move Turtle
    protected override void Update()
    {
        if (isFollowing)
        {
            turtleToTargetVector = targetTransform.position - transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

            // FIX: Feels messy
            if (targetTransform.tag == "Player" && distanceToTarget < 4) // If closer than 5 units to the Player
            {
                // TODO: Slow down smoothing as reaches player, rotate to face player even if stationary
                horizontalInput = 0;
                verticalInput = 0;
            }
            else if (targetTransform.tag == "Follower" && distanceToTarget < 3)
            {
                horizontalInput = 0;
                verticalInput = 0;
            }
            else
            {
                horizontalInput = turtleToTargetVector.x;
                verticalInput = turtleToTargetVector.z;
            }

            base.Update();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isFollowing)
        {
            InitFollow();

            if (!hasTarget)
            {
                if (FollowerID == 1) // If no other followers, follow the player
                {
                    targetTransform = PlayerController.PlayerTransform;
                    hasTarget = true;
                }
                else // Otherwise follow the last follower
                {
                    targetTransform = PlayerController.followers[PlayerController.followers.Count - 2].gameObject.transform;
                    hasTarget = true;
                }
            }
        }
    }
}
