using UnityEngine;

/// <summary>
/// Located on the camera itself and makes it move according to the mouse
/// </summary>

public class NouseMovement : MonoBehaviour
{
    //__________________________________________________________________________ Variables
    [SerializeField] private float mouseSensitivity = 400f;

    private float xMouseSensitivity;
    private float yMouseSensitivity;

    private float xMouseRotation;
    private float yMouseRotation;

    private Transform playerOrientation;

    //__________________________________________________________________________ Run
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerOrientation = GameObject.Find("PlayerOrientation").transform;
    }

    private void Update()
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

