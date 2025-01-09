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
    Transform shootingPoint;
    Transform aimAngel;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject aimCam;
    CinemachineBrain cineBrain;
    //GameObject aimCamScript;
    MouseMovement mouseMovement;

    //[SerializeField] float rotationSpeed;

    //__________________________________________________________________________ Run
    void Start()
    {
        Init();
    }


    void Update()
    {

            SincCamDirToPlayerMovement();
        if (Input.GetKey(KeyCode.Mouse1) == true)
        {
            //cineBrain.enabled = false;
           //player.transform.rotation = playerRotation;
           //mainCam.enabled = false;
            mainCam.SetActive(false);
            aimCam.SetActive(true);
            //aimCam.enabled = true;
            //transform.position = aimAngel.position;
            //MoveCamera();
        }

        else
        {
            mainCam.SetActive(true);
            aimCam.SetActive(false);
            //mainCam.enabled =true;
            //aimCam.enabled = false;
            //cineBrain.enabled = true;
        }
    }

    //__________________________________________________________________________ Mathods
    private void Init()
    {
        player = GameObject.Find("Player").transform;
        playerObj = GameObject.Find("Player_OBJ").transform;
        playerOrientation = GameObject.Find("PlayerOrientation").transform;
        shootingPoint = GameObject.Find("ShootingPoint").transform;
        aimAngel = GameObject.Find("playerCameraPos").transform;
        mouseMovement = gameObject.GetComponent<MouseMovement>();
        cineBrain = gameObject.GetComponent<CinemachineBrain>();
        //mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    Vector3 viewDiraction;
    Quaternion viewRotation;
    Vector3 inputDir;
    Ray ray;

    private void SincCamDirToPlayerMovement()
    {
        //player.rotation = transform.rotation;
        //playerRotation = transform.rotation;

        viewDiraction = player.position - new Vector3(transform.position.x , player.position.y , transform.position.z);
        playerOrientation.forward = viewDiraction.normalized;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        inputDir = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput ;

        if (inputDir != Vector3.zero)
        {
            //playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, rotationSpeed);
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, mouseSensitivity);
        }
    }

    [SerializeField] private float mouseSensitivity = 400f;

/*    private float xMouseSensitivity;
    private float yMouseSensitivity;

    private float xMouseRotation;
    private float yMouseRotation;

    Quaternion playerRotation;

    public void MoveCamera()
    {

        xMouseSensitivity = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yMouseSensitivity = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xMouseRotation -= yMouseSensitivity;
        yMouseRotation += xMouseSensitivity;

        xMouseRotation = Mathf.Clamp(xMouseRotation, -40, 40);

        transform.rotation = Quaternion.Euler(xMouseRotation, yMouseRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yMouseRotation, 0);
        playerObj.rotation = Quaternion.Euler(0, yMouseRotation, 0);

    }*/

}
