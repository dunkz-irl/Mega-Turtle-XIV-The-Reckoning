using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera mainCamera;
    CharacterController characterController;
    public float MovementSpeed = 1f;


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
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementVector = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        movementVector *= MovementSpeed;

        // Move player controller
        if (movementVector.magnitude >= 0.1f)
        {
            characterController.Move(movementVector * MovementSpeed * Time.deltaTime);            
        }        
    }
}
