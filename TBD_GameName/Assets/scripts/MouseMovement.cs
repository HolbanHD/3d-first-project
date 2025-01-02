using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Located on the camera itself and makes it move according to the mouse
/// </summary>

public class NouseMovement : MonoBehaviour
{
    //__________________________________________________________________________ Variables
    [SerializeField] private float mouseSensitivity = 400f;

    float xMouseSensitivity;
    float yMouseSensitivity;

    float xMouseRotation;
    float yMouseRotation;

    Transform playerOrientation;

    //__________________________________________________________________________ Run
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerOrientation = GameObject.Find("PlayerOrientation").transform;
    }

    void Update()
    {
        MoveCamera();
    }

    //__________________________________________________________________________ Mathods
    private void MoveCamera()
    {
        xMouseSensitivity = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yMouseSensitivity = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xMouseRotation -= yMouseSensitivity;
        yMouseRotation += xMouseSensitivity;

        //Limiting movement on the Y axis
        xMouseRotation = Mathf.Clamp(xMouseRotation, -90, 90);

        transform.rotation = Quaternion.Euler(xMouseRotation, yMouseRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yMouseRotation, 0);
    }
}

