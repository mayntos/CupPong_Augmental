using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    public BeerPongLogic bpLogicRef;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    { 
	}

    private void OnTriggerEnter(Collider other)
    {
        string theObjectTag = other.gameObject.tag;
        if (theObjectTag.Contains("Cup"))
        {
            bpLogicRef.IncrementScore();

            if (theObjectTag == ("centerCup"))
            {
                bpLogicRef.CanBullseye();
            }
            else if (theObjectTag == ("cornerCup"))
                bpLogicRef.IncrementCornerCount();
        }

        else if (theObjectTag.Contains("table"))
            bpLogicRef.DecrementCornerCount();
    }

}
