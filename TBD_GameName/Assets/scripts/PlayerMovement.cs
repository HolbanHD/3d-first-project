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
        onGround = Physics.CheckSphere(playerBoots.position , groundDistance, groundMask);
        

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
    }

}
