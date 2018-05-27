using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementScript : MonoBehaviour
{

    public TextMesh tmRef;
    private float duration = 1.0f;
    private float alpha = 0;

    private Color lerpedColor;
    private Color colorStart = Color.white;
    private Color colorEnd = new Color(1f, 1f, 1f, 0);

    private bool canFade;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (canFade)
        {
            lerpedColor = Color.Lerp(colorStart, colorEnd, Mathf.PingPong(Time.time, 1));
            tmRef.color = lerpedColor;
        }
    }

    public void CanFadeText(string s)
    {
        StartCoroutine(FadeText(s));
    }

    IEnumerator FadeText(string s)
    {
        tmRef.text = s;
        canFade = true;
        yield return new WaitForSeconds(3.0f);
        canFade = false;
        tmRef.text = "";
    }
}
