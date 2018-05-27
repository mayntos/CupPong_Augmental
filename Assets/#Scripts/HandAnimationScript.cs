using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationScript : MonoBehaviour
{
    public bool clenched;
    public Animator myAnim;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(clenched) { myAnim.SetBool("isPressed", true); }
        else if(!clenched) { myAnim.SetBool("isPressed", false); }
	}
}
