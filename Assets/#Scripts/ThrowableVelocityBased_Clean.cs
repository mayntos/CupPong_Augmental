using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vuforia
{
    public class ThrowableVelocityBased_Clean : MonoBehaviour, ITrackableEventHandler
    {

        public Transform palmTransform;
        private Rigidbody rbRef;

        public float grabbableDistance;

        //physics related variables
        public float handVelocityValue;
        public float throwForce;
        private bool hasPlayer = false;
        private bool beingCarried = false;
        private Vector3 previousPosition;

        //animation
        public HandAnimationScript haRef;

        //trackable behavior
        public GameObject trackingTarget;
        private TrackableBehaviour trackingRef;

        // Use this for initialization
        void Start()
        {
            rbRef = gameObject.GetComponent<Rigidbody>();
            trackingRef = trackingTarget.GetComponent<TrackableBehaviour>();
            if(trackingRef)
            {
                trackingRef.RegisterTrackableEventHandler(this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            float dist = Vector3.Distance(gameObject.transform.position, palmTransform.position);
            hasPlayer = (dist <= grabbableDistance) ? true : false;
            if (hasPlayer)
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
                //the ideal direction actually is:
                //palmTransform.position.z - previousPosition.z
                //testing position is:
                //previousPosition.z - palmTransform.z
                if (-(previousPosition.z - palmTransform.position.z) > handVelocityValue)
                {
                    haRef.clenched = false;
                    rbRef.isKinematic = false;
                    transform.parent = null;
                    beingCarried = false;

                    //release velocity should be (previousPosition - transform.position)?
                    Vector3 throwVelocity = (transform.position - previousPosition) / Time.deltaTime;
                    rbRef.AddForce(throwVelocity * throwForce, ForceMode.Impulse);
                    StartCoroutine(ReturnBall(3.0f));
                }
            }

            previousPosition = palmTransform.position;
        }

        private IEnumerator ReturnBall(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            rbRef.velocity = Vector3.zero;
            rbRef.angularVelocity = Vector3.zero;
            gameObject.transform.position = new Vector3(palmTransform.position.x, palmTransform.position.y, palmTransform.position.z);
        }

        public void OnTrackableStateChanged(TrackableBehaviour.Status previousState, TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED )
            {
                transform.position = new Vector3(palmTransform.position.x, palmTransform.position.y, palmTransform.position.z);
            }
        }
    }
}