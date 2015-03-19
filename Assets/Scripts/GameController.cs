using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour 
{
    public GameObject playerPrefab;
    public string spawnTag;
    public float fadeSpeed;

    static GameController singleton;
    public static GameController Get() { return singleton; }

    Texture2D blackText;
    float alpha = 1;
	
    void Awake()
    {
        singleton = this;
        OnLevelWasLoaded(1); //workaround since we still don't have main menu
        blackText = new Texture2D(1, 1);
        blackText.SetPixel(1, 1, Color.black);
        blackText.Apply();
    }

    void OnGUI()
    {
        if(alpha > 0)
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackText);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if(level != 0)
        {
            GameObject spawn = GameObject.FindGameObjectWithTag(spawnTag);
            GameObject.Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
            StartFadeIn(null);
        }
    }
    
    public void LoadLevel(int i)
    {
        Application.LoadLevel(i);
    }

    public void StartFadeIn(Action action)
    {
        StartCoroutine(FadeIn(action));
    }

    public void StartFadeOut(Action action)
    {
        StartCoroutine(FadeOut(action));
    }

    IEnumerator FadeOut(Action action)
    {
        while (alpha < 1)
        {
            alpha += fadeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (action != null)
            action();
    }

    IEnumerator FadeIn(Action action)
    {
        while (alpha > 0)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (action != null)
            action();
    }
}
