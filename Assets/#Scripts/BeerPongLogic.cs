using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Timers;
using UnityEngine.Networking;
using System;

public class BeerPongLogic : MonoBehaviour
{

    private const int MAX_SCORE_COUNT = 100;
    private const string SCENE_NAME = "Cup Pong";

    public int scoreCount;
    public int pointValue;
    public Text textRef;
    private int cornerCount = 0;
    private List<string> playerAchievements = new List<string>();

    public AchievementScript achievementDisplayRef;

    //JSON objects
    JSONObject body;
    JSONObject achievements;

    // Use this for initialization
    void Start ()
    {
        initJSON();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    //----------------------------------
    public void IncrementScore()
    {
        scoreCount += pointValue;
        if (scoreCount == (pointValue * 10))
            AddCleared();
        textRef.text = "score: " + scoreCount;
    }

    public void CanBullseye()
    {
        achievementDisplayRef.CanFadeText("ACHIEVEMENT:\n[BULLSEYE]");
        playerAchievements.Add("Bullseye");
        Debug.Log("Bullseye");
    }

    //----------------------------------
    public void IncrementCornerCount()
    {
        cornerCount++;
        if (cornerCount == 3)
        {
            Debug.Log("Corner King");
            achievementDisplayRef.CanFadeText("ACHIEVEMENT:\n[CORNERED]");
            playerAchievements.Add("Cornered");
            cornerCount = 0;
        }
    }

    public void DecrementCornerCount()
    {
        if(cornerCount >= 1)
            cornerCount--;
    }
    //-----------------------------------

    public void AddCleared()
    {
        Debug.Log("Cleared");
        achievementDisplayRef.CanFadeText("ACHIEVEMENT:\n[CLEARED]");
        playerAchievements.Add("Cleared");
        StartCoroutine(EndLevel());
    }

    public void EndGame()
    {
        //http request or cloud function would go here
    }

    IEnumerator EndLevel()
    {
        initJSON();
        Debug.Log(body.ToString());
        StartCoroutine(postRequest("https://us-central1-augmental-54fe6.cloudfunctions.net/onGameComplete", body.ToString()));
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("main");
    }

    //JSON Methods

    public void initJSON()
    {
        String currentTimeInRegularFormat = getTimeStamp(DateTime.Now);
        var currentTimeInTicks = DateTime.Now.Ticks;


        body = new JSONObject();
        achievements = new JSONObject(JSONObject.Type.ARRAY);
        //name field
        body.AddField("sceneName", SCENE_NAME);
        //time fields
        body.AddField("timeInTicks", currentTimeInTicks);
        body.AddField("timeRegular", currentTimeInRegularFormat);
        //uid field
        body.AddField("user", "0aEbizNmAjRC7I2Hx1pPmArF8UR2");
        body.AddField("sceneId", Guid.NewGuid().ToString());
        //score field
        body.AddField("score", scoreCount.ToString());
        body.AddField("maxScore", MAX_SCORE_COUNT);
        //achievement fields
        foreach(string s in playerAchievements)
        {
            achievements.Add(s);
        }
        body.AddField("achievements", achievements);

    }

    public static string getTimeStamp(DateTime value)
    {
        return value.ToString("yyyy-MM-dd HH:mm:ss");
    }

    IEnumerator postRequest(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

}
