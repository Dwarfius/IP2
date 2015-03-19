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
    Color currentColor;
    int pickupCount, enemyCount;
    public bool CanDefeatEnemies { get; private set; }
	
    void Awake()
    {
        singleton = this;
        OnLevelWasLoaded(1); //workaround since we still don't have main menu
        blackText = new Texture2D(1, 1);
        blackText.SetPixel(1, 1, Color.white);
        blackText.Apply();
    }

    void OnGUI()
    {
        if(alpha > 0)
        {
            GUI.depth = -1;
            GUI.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackText);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if(level != 0)
        {
            pickupCount = GameObject.FindGameObjectsWithTag("Pickup").GetLength(0);
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").GetLength(0);
            GameObject spawn = GameObject.FindGameObjectWithTag(spawnTag);
            GameObject.Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
            StartFadeIn(null, Color.black);
        }
    }
    
    public void LoadLevel(int i)
    {
        Application.LoadLevel(i);
    }

    public void StartFadeIn(Action action, Color fromColor)
    {
        currentColor = fromColor;
        StartCoroutine(FadeIn(action));
    }

    public void StartFadeOut(Action action, Color toColor)
    {
        currentColor = toColor;
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

    public void Pickup()
    {
        CanDefeatEnemies = pickupCount-- == 0;
    }
}
