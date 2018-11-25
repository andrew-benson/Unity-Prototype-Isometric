using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float PlayerSpeed = 5f;
    public static Rigidbody playerRigidbody; 

    float _sideMovement, _forwardMovement;
    Vector3 combinedMovementDirection;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
        _sideMovement = transform.position.x;
        _forwardMovement = transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        

    }

    private void FixedUpdate()
    {
        var leftX = Input.GetAxis("Horizontal");
        var leftY = Input.GetAxis("Vertical");
        var rightX = Input.GetAxis("Mouse X");
        var rightY = Input.GetAxis("Mouse Y");

        // Movement - Left Analogue Stick
        _sideMovement = PlayerSpeed * Time.deltaTime * leftX;
        _forwardMovement = PlayerSpeed * Time.deltaTime * leftY;
        combinedMovementDirection = new Vector3(_sideMovement, 1, _forwardMovement);

        Debug.Log($"Side: {_sideMovement}, Forward: {_forwardMovement}");

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
                playerRigidbody.MoveRotation(Quaternion.LookRotation(lookDirection));
        }

        // Assign the position
        Vector2 combine = new Vector2(combinedMovementDirection.x, combinedMovementDirection.z);

        if (combine != Vector2.zero)
            playerRigidbody.velocity = new Vector3(combinedMovementDirection.x, 0, combinedMovementDirection.z);

        //playerRigidbody.MovePosition(new Vector3(combinedMovementDirection.x, transform.position.y, combinedMovementDirection.z));
    }
}
