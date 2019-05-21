using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour {
    public float PlayerSpeed;
    public static Rigidbody playerRigidbody;
    public float gravity = -7;
    public Animator animator;
    public float leftX, leftY;
    public float playerVelocity;

    float _sideMovement, _forwardMovement;
    Vector3 combinedMovementDirection;
    public float rightX;
    public float rightY;
    private Vector2 L_ThumbstickVector;
    private Vector2 R_ThumbstickVector;

    private const string ANIM_PARAM_ISMOVINGLEFTSTICK = "isMovingLeftStick";
    private const string ANIM_PARAM_ISMOVINGRIGHTSTICK = "isMovingRightStick";
    private const string ANIM_PARAM_SPEED = "speed";
    private const string ANIM_PARAM_FRONT_BACK = "front-to-back";
    private const string ANIM_PARAM_LEFT_RIGHT = "left-to-right";

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
        LeftThumbStick();
        RightThumbStick();
        OnScreenDebug();

    }

    private void OnScreenDebug()
    {

        Debug.DrawLine(transform.position, transform.position + new Vector3(L_ThumbstickVector.x,0,L_ThumbstickVector.y), Color.cyan);
        Debug.DrawLine(transform.position, transform.position + new Vector3(R_ThumbstickVector.x, 0, R_ThumbstickVector.y), Color.red);
    }

    private void LeftThumbStick()
    {
        leftX = Input.GetAxis("Horizontal");
        leftY = Input.GetAxis("Vertical");

        L_ThumbstickVector = new Vector2(leftX, leftY);
        R_ThumbstickVector = new Vector2(rightX, rightY);

        var angle = -(Vector2.SignedAngle(R_ThumbstickVector * -1, L_ThumbstickVector * -1)) / 180;

        var signedThumbstickAngle = (Vector2.Dot(R_ThumbstickVector, L_ThumbstickVector)); // Left and right values should be 0 when vectors are same or 180 apart

        Debug.Log("Left/Right angle :" + angle);
        Debug.Log("Front/Back angle :" + signedThumbstickAngle);

        if (L_ThumbstickVector != Vector2.zero)
        {
            animator.SetBool(ANIM_PARAM_ISMOVINGLEFTSTICK, true);

            playerRigidbody.velocity = new Vector3(leftX * PlayerSpeed, playerRigidbody.velocity.y, leftY * PlayerSpeed);
            playerVelocity = new Vector3(leftX * PlayerSpeed, playerRigidbody.velocity.y, leftY * PlayerSpeed).magnitude;
            animator.SetFloat(ANIM_PARAM_SPEED, playerVelocity);
            animator.SetFloat(ANIM_PARAM_LEFT_RIGHT, angle);
            animator.SetFloat(ANIM_PARAM_FRONT_BACK, signedThumbstickAngle);

        }
        else
            animator.SetBool(ANIM_PARAM_ISMOVINGLEFTSTICK, false);

    }

    private void RightThumbStick()
    {
        rightX = Input.GetAxis("Mouse X");
        rightY = Input.GetAxis("Mouse Y");

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
            Vector3 lookDirection = new Vector3(leftX, 0, leftY);

            if (lookDirection != Vector3.zero)
            {
                playerRigidbody.MoveRotation(Quaternion.LookRotation(lookDirection));
            }        
        }

        if(R_ThumbstickVector != Vector2.zero)
            animator.SetBool(ANIM_PARAM_ISMOVINGRIGHTSTICK, true);
        else        
            animator.SetBool(ANIM_PARAM_ISMOVINGRIGHTSTICK, false);       
    }
}
