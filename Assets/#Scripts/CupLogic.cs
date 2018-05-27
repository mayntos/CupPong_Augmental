using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupLogic : MonoBehaviour
{

    private bool canFloat = false;
    private Vector3 origin;
    private Vector3 destination;
    // Use this for initialization

    public AudioSource audioRef;
	void Start ()
    {
        origin = gameObject.transform.position;
        destination = new Vector3(origin.x, origin.y + .1f, origin.z);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (canFloat)
            gameObject.transform.position = Vector3.Lerp(transform.position, destination, 1.5f * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            StartCoroutine(Scored());
        }
    }

    private IEnumerator Scored()
    {
        audioRef.Play();
        canFloat = true;
        yield return new WaitForSecondsRealtime(1.5f);
        gameObject.SetActive(false);
    }
}
