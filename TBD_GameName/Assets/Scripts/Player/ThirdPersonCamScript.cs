using UnityEngine;


/// <summary>
/// Located on the main camera itself and makes it move according to the mouse on tow cameras.
/// free camera is for roming and no code for that, only settings in the cinemachin.
/// aim camera is for aiming using other camera and code.
/// </summary>

namespace Player
{

    public class ThirdPersonCamScript : MonoBehaviour
    {

        //__________________________________________________________________________ Variables
        Transform player;

        //cameras
        [SerializeField] GameObject mainCam;
        [SerializeField] GameObject freeCam;
        [SerializeField] GameObject aimCam;

        //inputs for getting mouse axis
        float xMouseSensitivity;
        float yMouseSensitivity;

        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] bool camSwitched = true;
        Quaternion mouseRotation;

        //__________________________________________________________________________ Run
        void Start()
        {
            player = GameObject.Find("Player").transform;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {

            //on right mouse hold klick: Switching between cameras and on switch setting tah aim camera to the direction of the last camera.
            if (Input.GetKey(KeyCode.Mouse1) == true)
            {

                freeCam.SetActive(false);
                aimCam.SetActive(true);

                if (camSwitched == false)
                {
                    aimCam.transform.localRotation = mainCam.transform.localRotation;
                    player.transform.rotation = mainCam.transform.rotation;
                    //mouseRotation.x = mainCam.transform.eulerAngles.x; // do not turn on
                    mouseRotation.y = mainCam.transform.eulerAngles.y;

                    camSwitched = true;
                }

                AimCameraSync();
            }

            else
            {
                camSwitched = false;
                freeCam.SetActive(true);
                aimCam.SetActive(false);
            }
        }

        //__________________________________________________________________________ Methods

        //moves camera on mouse input and rotating player by it.
        public void AimCameraSync()
        {
            xMouseSensitivity = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            yMouseSensitivity = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            mouseRotation.x -= yMouseSensitivity;
            mouseRotation.y += xMouseSensitivity;

            // limits the angel of aim up an down.
            mouseRotation.x = Mathf.Clamp(mouseRotation.x, -40, 40);

            aimCam.transform.localRotation = Quaternion.Euler(mouseRotation.x, mouseRotation.y, 0);
            player.rotation = Quaternion.Euler(0, mouseRotation.y, 0);
        }
    }

    /*    Vector3 viewDirection;
        Vector3 inputDir;

        private void SyncCamDirToPlayerMovement()
        {
            viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            playerOrientation.forward = viewDirection.normalized;

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
}
