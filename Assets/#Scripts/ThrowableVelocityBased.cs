using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableVelocityBased : MonoBehaviour
{
    public Transform palmTransform;
    private Rigidbody rbRef;

    //physics related variables
    public float throwForce;
    bool hasPlayer = false;
    bool beingCarried = false;
    private bool touched = false;

    //physics - transfer momentum
    private Vector3 velocity;
    private Vector3 angularVelocity;

    private Vector3 previousPosition;

    public HandAnimationScript haRef;
    public ResetPosition rpRef;

	// Use this for initialization
	void Start ()
    {
        rbRef = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float dist = Vector3.Distance(gameObject.transform.position, palmTransform.position);
        hasPlayer = (dist <= 1.0f) ? true : false;
        if(hasPlayer)
        {
            haRef.clenched = true;
            rbRef.isKinematic = true;
            transform.parent = palmTransform;
            beingCarried = true;
        }
    }

    private void FixedUpdate()
    {
        if (beingCarried)
        {
           
            if ((previousPosition.z - palmTransform.position.z) > 0.1f)
            {
                haRef.clenched = false;
                rbRef.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                Vector3 throwVelocity = transform.position - previousPosition / Time.deltaTime;
                rbRef.AddForce(throwVelocity * throwForce, ForceMode.Impulse);
                StartCoroutine(ReturnBall());
            }
            else if(Input.GetKeyDown(KeyCode.L))
            {
                rbRef.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }

        previousPosition = palmTransform.position;
    }

    private IEnumerator ReturnBall()
    {
        yield return new WaitForSeconds(3);
        rpRef.ReturnBall();
    }
}
