using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class PlayerMovement : MonoBehaviour
{

    //__________________________________________________________________________ Variables
    private Rigidbody rb;

    //movement
    [SerializeField] private float currentSpeed;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float inAirMoveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float playerGravity;

    //jump
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    private bool canJump;

    //on ground chack
    [SerializeField] private float playerHeight;
    public LayerMask groundLayer;
    [SerializeField] private bool onGround;

    public Transform playerOrientation;

    private Vector3 moveDirection;

    private float horizontalInput;
    private float verticalInput;


    //__________________________________________________________________________ Run
    private void Start()
    {
        playerOrientation = transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        canJump = true;
        currentSpeed = defaultSpeed;
    }

    private void Update()
    {
        //ground chack, by shooting raycast down that finds a layer of ground
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        //adding drag on ground with Rigidbody
        if (onGround)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        PlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
        SpeedControl();
    }


    //__________________________________________________________________________ Mathods
    //gets input and sets different speeds 
    private void PlayerInput()
    {
        //gets directions based on input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //sprint mechanic
        if (Input.GetKeyDown(KeyCode.LeftShift) && canJump && onGround)
        {
            currentSpeed = sprintSpeed;
        }

        //Checks if the player is on the floor to reset the speed, for not to damage his inertia in the air after a jump while sprint jump.
        if (Input.GetKey(KeyCode.LeftShift) == false && onGround)
        {
            currentSpeed = defaultSpeed;
        }
    }

    //calculate and adding force to the diraction by inputs and ground chacking
    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        //adding force for movement on ground
        if (onGround)
        {
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force); 
        }

        //adding force for movement in air and gravity simulation
        else
        {
            rb.AddForce(moveDirection.normalized * inAirMoveSpeed * 10f * currentSpeed, ForceMode.Force);
            rb.AddForce(Vector3.down.normalized * playerGravity * 10f, ForceMode.Force); 
        }

    }

    //adds impulse force up, for jumping
    private void Jump()
    {
        //if (Input.GetKey(KeyCode.Space) && canJump && onGround)
        if(Input.GetButton("Jump") && canJump && onGround)
        {
            canJump = false;
            rb.AddForce(transform.up * jumpForce + moveDirection * currentSpeed, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    //calculate and limit the player speed
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > currentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }

}