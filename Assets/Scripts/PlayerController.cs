using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Turtle
{
    public static Transform PlayerTransform;
    public static List<FollowerController> followers = new List<FollowerController>();

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
            followers[0].SetMovementTarget(GameObject.FindGameObjectWithTag("PressurePlate").transform, 1f);
            //followers[0].isFollowing = false; // This makes things weird

            // Remove first follower from List  
            followers[0].FollowerID = 0;
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

        base.Update();
    }
}
