using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkingsound : MonoBehaviour
{
    CharacterController characterController;
    bool stepCycleStarted = false;
    bool isMoving;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    IEnumerator stepCycle()
    {
        walkingsound();

        yield return new WaitForSeconds(0.18f);

        if (isMoving)
            StartCoroutine(stepCycle());
        else
            StopCoroutine(stepCycle());
    }

    private void Update()
    {
        bool _isMoving = isMoving;

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 movementVector = new Vector3(horizontalInput, verticalInput);

        if (Mathf.Abs(movementVector.magnitude) > 0f)
        {            
            isMoving = true;
        }            
        else        
            isMoving = false;            

        if (_isMoving != isMoving)
        {
            StopAllCoroutines();            
            StartCoroutine(stepCycle());
        }
    }



    private void walkingsound(){
        AkSoundEngine.PostEvent("FOOTSTEPS", gameObject);
    }
    private void tinyfoot(){
         AkSoundEngine.PostEvent("TinyFootsteps", gameObject);
    }
}
