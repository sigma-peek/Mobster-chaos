using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private GameObject playerParent;
    private Vector2 inputVector, inputVectorSmooth, cameraCurrent;
    private float sensitivty = 0.5f, smoothing = 0.5f;

    void Awake()
    {
        playerParent = transform.parent.gameObject;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    { 
    }

    private void OnCameraLook(InputValue inputValue) {
        inputVector = inputValue.Get<Vector2>();
        inputVector = Vector2.Scale(inputVector, new Vector2(sensitivty * smoothing, sensitivty * smoothing));

        inputVectorSmooth.x = Mathf.Lerp(inputVectorSmooth.x, inputVector.x, 1f/smoothing);
        inputVectorSmooth.y = Mathf.Lerp(inputVectorSmooth.y, inputVector.y, 1f/smoothing);

        cameraCurrent += inputVectorSmooth;

        cameraCurrent.y = Mathf.Clamp (cameraCurrent.y, -30.0f, 30.0f);

        transform.localRotation = Quaternion.AngleAxis(-cameraCurrent.y, Vector3.right);

        playerParent.transform.localRotation = Quaternion.AngleAxis(cameraCurrent.x, playerParent.transform.up);
    }
}
