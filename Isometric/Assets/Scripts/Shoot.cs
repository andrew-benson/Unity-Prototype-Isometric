using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject prefab;
    public Transform prefabOrigin;
	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        var fire = Input.GetAxis("Fire");

        if (fire > 0) 
        {
            GameObject projectile = Instantiate(prefab, prefabOrigin.position, transform.rotation);
        }
    }
}
