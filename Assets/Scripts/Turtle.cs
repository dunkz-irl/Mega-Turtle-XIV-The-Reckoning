using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    public float MovementSpeed = 1f;
    public float RotationSmoothTime = 0.1f;
    public float JumpHeight = 1f;
    public float Gravity = -9.81f;
    public bool CanDoubleJump = false;
    public int MaximumJumps = 2;
    public bool isGrounded;
    
    protected float verticalInput;
    protected float horizontalInput;

    private Vector3 inputVector;
    private CharacterController characterController;
    private float currentVelocity;    
    private int jumpCounter;
    private float targetAngle;
    [SerializeField] private Vector3 jumpVector = new Vector3();

    protected void InitTurtle()
    {
        characterController = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {
        inputVector = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        inputVector *= MovementSpeed;

        // Player movement
        if (inputVector.magnitude >= 0.1f)
        {
            // Rotate turtle to face in direction of movement
            RotateTurtle();

            // Move turtle
            Vector3 movementVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(movementVector.normalized * MovementSpeed * Time.deltaTime);
        }

        jumpVector.y += Gravity * Time.deltaTime;
        characterController.Move(jumpVector * Time.deltaTime); // For some reason this has to be called before accessing characterController.isGrounded or jumping is buggy when moving

        isGrounded = characterController.isGrounded;
        if (characterController.isGrounded && jumpVector.y <= 0)
        {
            jumpVector.y = 0f;
            jumpCounter = 0; // Reset double jump counter
        }
    }

    protected void RotateTurtle()
    {        
        if (gameObject.tag == "Player")
            targetAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y; // Add camera Vector for player
        else
            targetAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg; // But not for followers

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, RotationSmoothTime); // To smooth player y rotation
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    internal void Jump()
    {
        if (isGrounded)
        {
            jumpVector.y += Mathf.Sqrt(JumpHeight * -2.0f * Gravity);
            jumpCounter++;            

            // TODO: sound effect, animation
        }

        else // Double Jump
        {
            if (CanDoubleJump && jumpCounter < MaximumJumps)
            {
                jumpVector.y += Mathf.Sqrt(JumpHeight * -2.0f * Gravity);
                jumpCounter++;

                // TODO: Particle effect, sound effect
                // FIX: maths for double jump height, currently second jump force depends on height when double jump is started
            }
        }

    }
}
