using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float PlayerSpeed = 5f;

    float sideMovement, forwardMovement;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        sideMovement += PlayerSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        forwardMovement += PlayerSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.position = new Vector3(sideMovement, transform.position.y, forwardMovement);
        

        Debug.Log("MOUSE X: " + Input.GetAxis("Mouse X"));
        Debug.Log("MOUSE Y " + Input.GetAxis("Mouse Y"));

        if(Input.GetAxis("Jump") > 0)
        {
            Debug.Log("Pressed Jump");
        }
    }
}
