using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Turtle
{
    public static Transform PlayerTransform;
    public static List<FollowerController> followers = new List<FollowerController>();
    public PressurePlate selectedPressurePlate;

    // Start is called before the first frame update
    void Start()
    {
        InitTurtle();
        PlayerTransform = transform;        
    }    

    // Update is called once per frame
    protected override void Update()
    {
        // Get Input Axis and create vector        
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            Jump();

            // Follower jump
            float jumpDelayFloat = 0.15f;
            float _jumpDelayFloat = 0.15f;

            foreach (var follower in followers)
            {
                WaitForSeconds jumpDelay = new WaitForSeconds(jumpDelayFloat);
                jumpDelayFloat += _jumpDelayFloat;

                IEnumerator followerJump()
                {
                    yield return jumpDelay;
                    follower.Jump();
                }

                StartCoroutine(followerJump());
            }
        }

        // Assign Follower
        if (Input.GetKeyDown(KeyCode.E))
        {
            // TODO: Check which Pressure plate, and if empty
            //       Also for turtles returning to player
            // FIX:  isFollowing reset             

            // Send first follower to target
            if (selectedPressurePlate != null && !selectedPressurePlate.isOccupied) // FIX: Can probs make this check less messy somehow
            {
                followers[0].SetMovementTarget(selectedPressurePlate.transform, 1f);
                //followers[0].isFollowing = false; // This makes things weird

                selectedPressurePlate.isOccupied = true; // TODO: could be on event?

                // Remove first follower from List and tell the pressure plate who's occupying it
                followers[0].FollowerID = 0;
                selectedPressurePlate.Occupier = followers[0];
                followers.RemoveAt(0);

                if (followers.Count != 0)
                {
                    // Send next turtle in queue to follow player
                    followers[0].SetMovementTarget(PlayerTransform, 4f);
                }            

                // Reset Follower IDs
                int i = 1;
                foreach (var follower in followers)
                {
                    follower.FollowerID = i;
                    i++;
                }
            }

            else if (selectedPressurePlate != null && selectedPressurePlate.isOccupied)
            {
                Debug.Log("Turtle come back!!!!");

                selectedPressurePlate.Occupier.InitFollow();
                selectedPressurePlate.isOccupied = false;
            }
        }

        base.Update();
    }
}