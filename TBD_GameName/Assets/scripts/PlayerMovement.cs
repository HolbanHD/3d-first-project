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
    Rigidbody rb;

    //movement
    [SerializeField] float moveSpeed;
    [SerializeField] float groundDrag;

    //jump
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool readyToJump;

    //[SerializeField] float walkSpeed;
    //[SerializeField] float sprintSpeed;

    //public KeyCode jumpKey = KeyCode.Space;

    [SerializeField] float playerHeight;
    public LayerMask whatIsGround;
    [SerializeField] bool grounded;

    public Transform playerOrientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;


    //__________________________________________________________________________ Run
    private void Start()
    {
        playerOrientation = GameObject.Find("PlayerOrientation").transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        PlayerInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    //__________________________________________________________________________ Mathods
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetButton("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

}

    /*//__________________________________________________________________________ Variables
    [SerializeField] CharacterController Controller;

    [SerializeField] float playerSpeed = 12f;
    [SerializeField] float playerGravity = -9.81f * 2;
    [SerializeField] float playerJumpHeight = 3f;
    [SerializeField] float moveDirX;
    [SerializeField] float moveDirZ;
    [SerializeField] Vector3 velocity;

    [SerializeField] Transform playerBoots;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] bool onGround;

    //__________________________________________________________________________ Run
    void Start()
    {
        //Controller = GetComponent<CharacterController>();
        Controller = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        JumpPlayer();
    }

    private void Update()
    {
        IsPlayerOnGroundChack();
    }

    //__________________________________________________________________________ Mathods

    private void IsPlayerOnGroundChack()
    {
        onGround = Physics.CheckSphere(playerBoots.position, groundDistance, groundMask);


        if (onGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void MovePlayer()
    {

        moveDirX = Input.GetAxis("Horizontal");
        moveDirZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveDirX + transform.forward * moveDirZ;

        Controller.Move(move * playerSpeed * Time.deltaTime);

    }

    private void JumpPlayer()
    {

        if (Input.GetButton("Jump") && onGround == true)
        {
            velocity.y = Mathf.Sqrt(playerJumpHeight * -2f * playerGravity);
        }
        velocity.y += playerGravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);
    }*/