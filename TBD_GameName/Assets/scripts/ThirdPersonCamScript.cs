using Cinemachine;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Located on the camera itself and makes it move according to the mouse
/// </summary>

public class ThirdPersonCamScript : MonoBehaviour
{

    //__________________________________________________________________________ Variables
    Transform player;
    Transform playerObj;
    Transform playerOrientation;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject aimCam;

    [SerializeField] private float mouseSensitivity = 400f;

    //Quaternion cameraRotation;
    //Quaternion playerRotation;

    //__________________________________________________________________________ Run
    void Start()
    {
        Init();
    }

    void Update()
    {


        if (Input.GetKey(KeyCode.Mouse1) == true)
        {
            mainCam.SetActive(false);
            aimCam.SetActive(true);
            //aimCam.transform.rotation = cameraRotation;
            //playerOrientation.rotation = playerRotation;
            AimCameraSync();
        }

        else
        {
            mainCam.SetActive(true);
            aimCam.SetActive(false);
            SyncCamDirToPlayerMovement();

        }
    }

    //__________________________________________________________________________ Mathods
    private void Init()
    {
        player = GameObject.Find("Player").transform;
        playerObj = GameObject.Find("Player_OBJ").transform;
        playerOrientation = GameObject.Find("PlayerOrientation").transform;
        Cursor.lockState = CursorLockMode.Locked;

    }

    Vector3 viewDiraction;
    Vector3 inputDir;

    private void SyncCamDirToPlayerMovement()
    {
        viewDiraction = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        playerOrientation.forward = viewDiraction.normalized;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        inputDir = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, mouseSensitivity);
        }

        //cameraRotation = mainCam.transform.rotation;
        //playerRotation = playerOrientation.rotation;

    }


    float xMouseSensitivity;
    float yMouseSensitivity;
    float xMouseRotation;
    float yMouseRotation;

    public void AimCameraSync()
    {

        xMouseSensitivity = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yMouseSensitivity = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xMouseRotation -= yMouseSensitivity;
        yMouseRotation += xMouseSensitivity;

        xMouseRotation = Mathf.Clamp(xMouseRotation, -40, 40);

        transform.rotation = Quaternion.Euler(xMouseRotation, yMouseRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yMouseRotation, 0);
        //playerObj.rotation = Quaternion.Euler(0, yMouseRotation, 0);

        //cameraRotation = aimCam.transform.rotation;
        //playerRotation = playerOrientation.rotation;
    }

}
