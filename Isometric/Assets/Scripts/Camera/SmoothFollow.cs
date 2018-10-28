// Smooth Follow from Standard Assets
// Converted to C# because I fucking hate UnityScript and it's inexistant C# interoperability
// If you have C# code and you want to edit SmoothFollow's vars ingame, use this instead.
using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{

    // The target we are following
    public Transform target;

    // the height we want the camera to be above the target
    public float height = 5.0f;
    public float zDistance = 8f;
    // How much we 
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    // Place the script in the Camera-Control group in the component menu
    [AddComponentMenu("Camera-Control/Smooth Follow")]

    // Place the script in the Camera-Control group in the component menu
    private void Start()
    {
        // Always look at the target
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target) return;

        // Calculate the current rotation angles
        float wantedHeight = target.position.y + height;
        float wantedDistance = transform.position.z - zDistance;

        float currentHeight = transform.position.y;
        float currentDistanceZ = transform.position.z;
        float currentDistanceX = transform.position.x;

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        currentDistanceZ = Mathf.Lerp(currentDistanceZ, target.position.z - zDistance, heightDamping * Time.deltaTime);
        currentDistanceX = Mathf.Lerp(currentDistanceX, target.position.x, heightDamping * Time.deltaTime);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;

        // Set the height of the camera
        transform.position = new Vector3(currentDistanceX, currentHeight, currentDistanceZ);



        Debug.Log("damping " + heightDamping * Time.deltaTime);

    }
}