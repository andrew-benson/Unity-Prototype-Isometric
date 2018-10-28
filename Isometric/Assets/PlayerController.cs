using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float PlayerSpeed = 5f;

    float _sideMovement, _forwardMovement;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var leftX = Input.GetAxis("Horizontal");
        var leftY = Input.GetAxis("Vertical");
        var rightX = Input.GetAxis("Mouse X");
        var rightY = Input.GetAxis("Mouse Y");

        // Movement - Left Analogue Stick
        _sideMovement += PlayerSpeed * Time.deltaTime * leftX;
        _forwardMovement += PlayerSpeed * Time.deltaTime * leftY;
        var combinedMovementDirection = new Vector3(_sideMovement, transform.position.y, _forwardMovement);

        // Look rotation - Right Analogue Stick
        if (rightX != 0 || rightY != 0)
        {
            Vector3 lookDirection = new Vector3(rightX, 0, rightY);
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
        else
        {
            // Use the left stick to create look rotation
            Vector3 lookDirection = new Vector3(leftX, 0, leftY);
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }


        // Assign the position
        transform.position = combinedMovementDirection;
    }
}
