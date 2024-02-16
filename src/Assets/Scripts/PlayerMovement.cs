using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    private Vector2 inputVector;
    private Vector3 moveVector;
    private float playerSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move(playerSpeed * moveVector * Time.deltaTime);
    }

    private void OnMovement(InputValue inputValue) {
        inputVector = inputValue.Get<Vector2>();
        moveVector.x = inputVector.x;
        moveVector.z = inputVector.y;
    }
}
