using Cinemachine;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

/// <summary>
/// Located on the camera itself and makes it move according to the mouse
/// </summary>

public class ThirdPersonCamScript : MonoBehaviour
{

    //__________________________________________________________________________ Variables
    Transform player;

    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject freeCam;
    [SerializeField] GameObject aimCam;

    [SerializeField] private float mouseSensitivity = 400f;

    [SerializeField] bool camSwitched = true;

    Vector3 mainCamTrans;

    //__________________________________________________________________________ Run
    void Start()
    {
        player = GameObject.Find("Player").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {

    }

    void Update()
    {
        //mainCamTrans = mainCam.transform.rotation;


        if (Input.GetKey(KeyCode.Mouse1) == true)
        {

            freeCam.SetActive(false);
            //aimCam.SetActive(true);
                AimCameraSync();

            if (camSwitched == true)
            {
                aimCam.transform.localRotation = mainCam.transform.rotation;
                player.transform.rotation = mainCam.transform.rotation;

                //mouseRotation = mainCam.transform.rotation;

                mouseRotation.x = mainCam.transform.eulerAngles.x;
                mouseRotation.y = mainCam.transform.eulerAngles.y;


                //Invoke(nameof(zzzzzz), 3);
                camSwitched = false;
            }
        }

        else
        {
            camSwitched = true;
            freeCam.SetActive(true);
            //aimCam.SetActive(false);
        }
    }

    //__________________________________________________________________________ Mathods

    private void zzzzzz()
    {
        camSwitched = false;
    }


    float xMouseSensitivity;
    float yMouseSensitivity;

    //public float xMouseRotation;
    //public float yMouseRotation;

    Quaternion mouseRotation;

    public void AimCameraSync()
    {

        //float x = 

        xMouseSensitivity = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yMouseSensitivity = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        mouseRotation.x -= yMouseSensitivity;
        mouseRotation.y += xMouseSensitivity;

        mouseRotation.x = Mathf.Clamp(mouseRotation.x, -40, 40);

        aimCam.transform.rotation = Quaternion.Euler(mouseRotation.x, mouseRotation.y, 0);
        player.rotation = Quaternion.Euler(0, mouseRotation.y, 0);

    }


    /*    public void AimCameraSync()
        {

            xMouseSensitivity = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            yMouseSensitivity = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xMouseRotation -= yMouseSensitivity;
            yMouseRotation += xMouseSensitivity;

            xMouseRotation = Mathf.Clamp(xMouseRotation, -40, 40);

            aimCam.transform.rotation = Quaternion.Euler(xMouseRotation, yMouseRotation, 0);
            player.rotation = Quaternion.Euler(0, yMouseRotation, 0);

        }*/

}

/*    Vector3 viewDiraction;
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
    }*/


/*    private void xxx()
    {

        camAxis.x = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        camAxis.y = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;

        camAxis.x = Mathf.Clamp(camAxis.x, -40, 40);



        player.rotation = Quaternion.Euler(0, yMouseRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yMouseRotation, 0);

        //transform.localRotation = Quaternion.Euler(-camAxis.y, +camAxis.x, 0);

        if (camAxis != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(camAxis, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed);
        }
    }*/