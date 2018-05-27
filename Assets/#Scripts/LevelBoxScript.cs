﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBoxScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("test2");
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
