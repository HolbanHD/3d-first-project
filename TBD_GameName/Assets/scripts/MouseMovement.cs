using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class NouseMovement : MonoBehaviour
{

    [SerializeField] private float mouseSensitivity = 800f;

    float xMousePos;
    float yMousePos;

    float xRotation;
    float yRotation;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        xMousePos = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yMousePos = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= yMousePos;
        yRotation += xMousePos;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
