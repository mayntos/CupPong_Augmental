using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

    public GameObject ball;
    public Vector3 spawnLocation;
    public Transform palm;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        spawnLocation = palm.position;
	}

    public void ReturnBall()
    {
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.position = new Vector3(spawnLocation.x, spawnLocation.y, spawnLocation.z);    
    }
}
