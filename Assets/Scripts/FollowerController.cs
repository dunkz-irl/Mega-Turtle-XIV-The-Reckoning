using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : Turtle
{
    public bool isAwake = false;
    public bool hasReachedDestination;
    public int FollowerID;

    private Vector3 turtleToTargetVector;
    public bool isFollowingPlayer;
    private GameObject playerGO;
    [SerializeField] private Transform targetTransform;
    public float targetStopDistance = 2f;
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitTurtle();
        Jump();
        isGrounded = true;
    }

    public void InitFollowPlayer()
    {        
        isAwake = true;
        PlayerController.followers.Add(this); 
         AkSoundEngine.PostEvent("Pickupturtle", gameObject);//  Add this turtle follower to the list of followers on the PlayerController script
        FollowerID = PlayerController.followers.Count;

        Debug.Log("Follow Initialised, isFollowingPlayer is " + isFollowingPlayer + ", ID is " + PlayerController.followers.Count);

        if (PlayerController.endInitiated) // FIX: Null reference here
        {
            return;
        }

        if (!isFollowingPlayer || targetTransform.GetComponent<PressurePlate>() != null)
        {
            if (FollowerID == 1) // If no other followers, follow the player
            {
                targetTransform = PlayerController.PlayerTransform;
                isFollowingPlayer = true;
                targetStopDistance = 1.25f;
            }
            else // Otherwise follow the last follower
            {
                targetTransform = PlayerController.followers[PlayerController.followers.Count - 2].gameObject.transform;
                isFollowingPlayer = true;
                targetStopDistance = 1.1f;
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
        isGrounded = false;
        base.animator.SetBool("isHiding", false);
    }

    // Set horizontalInput and verticalInput to move Turtle
    protected override void Update()
    {
        if (isAwake)
        {
            if (PlayerController.endInitiated == true) // For final follow
                return;

            float distanceToTarget = 0f;
            
            turtleToTargetVector = targetTransform.position - transform.position;
            distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);


            // FIX: Checking this way feels messy
            if (targetTransform.tag == "Player" && distanceToTarget < targetStopDistance) // If closer than 4 units to the Player
            {
                // TODO: Slow down smoothing as reaches player, rotate to face player even if stationary
                horizontalInput = 0;
                verticalInput = 0;
                hasReachedDestination = true;
            }
            else if (targetTransform.tag == "Follower" && distanceToTarget < targetStopDistance)
            {
               
                horizontalInput = 0;
                verticalInput = 0;
                hasReachedDestination = true;
            }
            else if (targetTransform.tag == "PressurePlate" && distanceToTarget < targetStopDistance)
            {
                horizontalInput = 0;
                verticalInput = 0;
                if(hasReachedDestination == false){ AkSoundEngine.PostEvent("TILE", gameObject);}
                hasReachedDestination = true;
                base.animator.SetBool("isHiding", true);
              
               
            }
            else
            {
                horizontalInput = turtleToTargetVector.x;
                verticalInput = turtleToTargetVector.z;
                hasReachedDestination = false;
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
    public void FinalFollow(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
