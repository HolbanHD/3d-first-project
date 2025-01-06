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

    [SerializeField] float rotationSpeed;

    //__________________________________________________________________________ Run
    void Start()
    {
        Init();
    }


    void Update()
    {
        SincCamDirToPlayerMovement();
    }

    //__________________________________________________________________________ Mathods
    private void Init()
    {
        player = GameObject.Find("Player").transform;
        playerObj = GameObject.Find("Player_OBJ").transform;
        playerOrientation = GameObject.Find("PlayerOrientation").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SincCamDirToPlayerMovement()
    {
        Vector3 viewDiraction = player.position - new Vector3(transform.position.x , player.position.y , transform.position.z);
        playerOrientation.forward = viewDiraction.normalized;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput ;

        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, rotationSpeed);
        }
    }

}
