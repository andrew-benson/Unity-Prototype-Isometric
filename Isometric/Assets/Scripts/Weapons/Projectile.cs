using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 10;

	// Use this for initialization
	void Start () {
        Invoker.Instance.InvokeAction(() =>
        {
            Destroy(gameObject);
        }, 2);
    }
    
    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
