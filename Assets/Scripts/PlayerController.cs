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

        base.Update();
    }
}
