using Assets.Scripts.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    #region Gameplay Variables
    public float PlayerSpeed;
    public float gravity = -7;
    public float playerVelocity;
    public float maxNeutralVelocity = 10;
    public float maxAimVelocity = 8;
    private Rigidbody playerRigidbody;
    #endregion

    [Space(10)]

    #region Input Variables 
    public float leftX;
    public float leftY;
    public float rightX;
    public float rightY;
    private Vector2 L_ThumbstickVector;
    private Vector2 R_ThumbstickVector;
    #endregion

    private Animator animator;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
        leftX = Input.GetAxis(InputPresets.HORIZONTAL);
        leftY = Input.GetAxis(InputPresets.VERTICAL);
        L_ThumbstickVector = new Vector2(leftX, leftY);

        // Get relationship between the thumbsticks to determine sideways movement
        var dotProduct = (Vector2.Dot(R_ThumbstickVector, Vector2.Perpendicular(L_ThumbstickVector)));

        // Get cross product for back and forth
        var signedThumbstickAngle = (Vector2.Dot(R_ThumbstickVector, L_ThumbstickVector));

        if (L_ThumbstickVector != Vector2.zero)
        {   
            // Set velocity of rigid body, which is scaled by player speed
            playerRigidbody.velocity = new Vector3(leftX * PlayerSpeed, playerRigidbody.velocity.y, leftY * PlayerSpeed);
            playerVelocity = new Vector3(leftX * PlayerSpeed, playerRigidbody.velocity.y, leftY * PlayerSpeed).magnitude;

            animator.SetBool(AnimationParams.IS_MOVING_LEFTSTICK, true);
            animator.SetFloat(AnimationParams.SPEED, playerVelocity);
            animator.SetFloat(AnimationParams.LEFT_RIGHT, dotProduct);
            animator.SetFloat(AnimationParams.FRONT_BACK, signedThumbstickAngle);        
        }
        else
            animator.SetBool(AnimationParams.IS_MOVING_LEFTSTICK, false);

    }

    private void RightThumbStick()
    {
        rightX = Input.GetAxis(InputPresets.MOUSE_X);
        rightY = Input.GetAxis(InputPresets.MOUSE_Y);
        R_ThumbstickVector = new Vector2(rightX, rightY);

        var isAiming = R_ThumbstickVector != Vector2.zero;
        var lookDirection = isAiming ? new Vector3(rightX, 0, rightY) : new Vector3(leftX, 0, leftY);
        PlayerSpeed = isAiming && lookDirection != Vector3.zero ? maxAimVelocity : maxNeutralVelocity;

        // Rotate only when direction isn't zero
        if(lookDirection != default)
            playerRigidbody.MoveRotation(Quaternion.LookRotation(lookDirection));

        // Enables transition in blend tree
        animator.SetBool(AnimationParams.IS_MOVING_RIGHTSTICK, isAiming);
    }
}
