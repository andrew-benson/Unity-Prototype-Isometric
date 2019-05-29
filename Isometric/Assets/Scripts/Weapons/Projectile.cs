using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 10;

	// Use this for initialization
	void Start ()
    {
        KillBullet();
    }

    private void KillBullet()
    {
        Invoker.Instance.InvokeAction(() =>
        {
            try
            {
                Destroy(gameObject);            
            }
            catch (MissingReferenceException ex)
            {
                Debug.Log(ex.Message);
            }
        }, 2);
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(gameObject.transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet Hit Something");
        Destroy(gameObject);
    }
}
