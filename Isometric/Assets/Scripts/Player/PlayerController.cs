using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float PlayerSpeed = 5f;
    public static Rigidbody playerRigidbody;
    public float gravity = -7;
    public Animator animator;
    public float xInput, yInput;
    public Vector3 playerVelocity;

    float _sideMovement, _forwardMovement;
    Vector3 combinedMovementDirection;
    public float rightX;
    public float rightY;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        _sideMovement = transform.position.x;
        _forwardMovement = transform.position.z;
    }


    private void FixedUpdate()
    {   
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        
        animator.SetFloat("L_BlendY", Math.Abs(yInput));   
        animator.SetFloat("L_BlendX", Math.Abs(xInput));
        
        rightX = Input.GetAxis("Mouse X");
        rightY = Input.GetAxis("Mouse Y");

        // Movement - Left Analogue Stick
        _sideMovement = PlayerSpeed * Time.deltaTime * xInput;
        _forwardMovement = PlayerSpeed * Time.deltaTime * yInput;
        combinedMovementDirection = new Vector3(_sideMovement, 1, _forwardMovement);
        
        // Look rotation - Right Analogue Stick
        if (rightX != 0 || rightY != 0)
        {
            Vector3 lookDirection = new Vector3(rightX, 0, rightY);
            if (lookDirection != Vector3.zero)
                playerRigidbody.MoveRotation(Quaternion.LookRotation(lookDirection));
        }
        else
        {
            // Use the left stick to create look rotation
            Vector3 lookDirection = new Vector3(xInput, 0, yInput);

            if (lookDirection != Vector3.zero)
                playerRigidbody.MoveRotation(Quaternion.LookRotation(lookDirection));
        }

        // Assign the position
        Vector2 combine = new Vector2(combinedMovementDirection.x, combinedMovementDirection.z);
        combine = combine.normalized * PlayerSpeed;

        if (combine != Vector2.zero)
        {
            playerRigidbody.velocity = new Vector3(combine.x * Mathf.Abs(xInput), playerRigidbody.velocity.y, combine.y * Mathf.Abs(yInput));
            playerVelocity = playerRigidbody.velocity;
        }

        //playerRigidbody.MovePosition(new Vector3(combinedMovementDirection.x, transform.position.y, combinedMovementDirection.z));
    }
}
