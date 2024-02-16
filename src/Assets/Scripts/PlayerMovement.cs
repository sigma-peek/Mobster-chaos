using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] GameObject playerCamera;
    private Vector2 inputVector;
    private Vector3 moveVector, forwardVector, rightVector, gravityVector;
    private float playerSpeed = 6;

    // Start is called before the first frame update
    void Awake()
    {       
        gravityVector = Vector3.zero; 
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move(playerSpeed * moveVector * Time.deltaTime);

        gravityVector.y += -20 * Time.deltaTime;
        controller.Move(gravityVector * Time.deltaTime);
    }

    private void OnMovement(InputValue inputValue) {
        inputVector = inputValue.Get<Vector2>();
        moveVector.x = inputVector.x;
        moveVector.z = inputVector.y;

        forwardVector = playerCamera.transform.forward * inputVector.y;
        rightVector = playerCamera.transform.right * inputVector.x;

        moveVector = forwardVector + rightVector;
    }
}
