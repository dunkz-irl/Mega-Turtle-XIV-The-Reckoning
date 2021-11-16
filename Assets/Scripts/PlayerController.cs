using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera mainCamera;
    CharacterController characterController;
    public float MovementSpeed = 1f;
    public float RotationSmoothTime = 0.1f;
    float currentVelocity;


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
    }
}
