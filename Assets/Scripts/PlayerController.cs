using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float MovementSpeed = 1f;
    public float RotationSmoothTime = 0.1f;
    public float JumpHeight = 1f;
    public float Gravity = -9.81f;
    public bool CanDoubleJump = false;
    public int MaximumJumps = 2;
    public bool isGrounded;

    Camera mainCamera;
    CharacterController characterController;
    float currentVelocity;
    [SerializeField]Vector3 jumpVector = new Vector3();
    int jumpCounter;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get Input Axis and create vector        
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 inputVector = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        inputVector *= MovementSpeed;

        // Player movement
        if (inputVector.magnitude >= 0.1f)
        {
            // Rotate player to face in direction of movement
            float targetAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, RotationSmoothTime); // To smooth player y rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move player
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

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpVector.y += Mathf.Sqrt(JumpHeight * -2.0f * Gravity);
            jumpCounter++;

            Debug.Log("Jump");

            // TODO: sound effect, animation
        }

        // Double Jump
        if (Input.GetButtonDown("Jump") && !isGrounded)
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
