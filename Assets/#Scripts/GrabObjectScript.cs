using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectScript : MonoBehaviour
{

    public Transform player;          //transform of Player gameobject
    public float throwForce;          //value for force to be applied when object is let go.
    Rigidbody rbRef;

    //flags for pickup object
    bool hasPlayer = false;
    bool beingCarried = false;

    private bool touched = false;     //may not need this

    float dist;
    public float grabRange;

    private void Awake()
    {
        rbRef = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start ()
    {
        dist = Vector3.Distance(gameObject.transform.position, player.position);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // if a player is within a certain distance, set it up to be grab-able.
        hasPlayer = (dist < grabRange) ? true : false;

        // assuming player is in range, if they press input button
        // they can pick up this game object.
        if(hasPlayer && Input.GetButtonDown("Use"))
        {
            rbRef.isKinematic = true;
            transform.parent = player;
            beingCarried = true;
        }

        if(beingCarried && Input.GetKeyDown(KeyCode.K))
        {
            rbRef.isKinematic = false;
            transform.parent = null;
            beingCarried = false;
        }

	}

}
