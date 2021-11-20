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
    public float targetStopDistance { get; set; } = 2f;

    private void Start()
    {
        InitTurtle();
        Jump();
    }

    public void InitFollow()
    {        
        isFollowing = true;
        PlayerController.followers.Add(this); //  Add this turtle follower to the list of followers on the PlayerController script
        FollowerID = PlayerController.followers.Count;

        Debug.Log("Follow Initialised, isFollowing is " + isFollowing + ", ID is " + PlayerController.followers.Count);

        if (!hasTarget || targetTransform.GetComponent<PressurePlate>() != null)
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

        halfJump();
    }

    internal void halfJump()
    {
        isGrounded = true; //  Don't know why this has to be here but it does ;P
        float _jh = JumpHeight;
        JumpHeight = 2.5f;
        Jump(); // Jump a bit to make it a bit more obvious they are following something
        JumpHeight = _jh;
    }

    // Set horizontalInput and verticalInput to move Turtle
    protected override void Update()
    {
        if (isFollowing)
        {
            turtleToTargetVector = targetTransform.position - transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

            // FIX: Checking this way feels messy
            if (targetTransform.tag == "Player" && distanceToTarget < targetStopDistance) // If closer than 4 units to the Player
            {
                // TODO: Slow down smoothing as reaches player, rotate to face player even if stationary
                horizontalInput = 0;
                verticalInput = 0;
            }
            else if (targetTransform.tag == "Follower" && distanceToTarget < targetStopDistance)
            {
                horizontalInput = 0;
                verticalInput = 0;
            }
            else if (targetTransform.tag == "PressurePlate" && distanceToTarget < targetStopDistance)
            {
                horizontalInput = 0;
                verticalInput = 0;
            }
            else
            {
                horizontalInput = turtleToTargetVector.x;
                verticalInput = turtleToTargetVector.z;
            }

            //Debug.Log("Stop distance: " + targetStopDistance + ". Distance to target: " + distanceToTarget);

            base.Update();
        }
    }

    // Moved to script on child GO
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player" && !isFollowing)
    //    {
    //        InitFollow();
    //    }
    //}

    public void SetMovementTarget(Transform transform, float stopDistance)
    {
        targetTransform = transform;
        targetStopDistance = stopDistance;
    }
}
