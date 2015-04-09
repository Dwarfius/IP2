using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    public GameObject playerPrefab;
    public GameObject uiPrefab;
    public string spawnTag;
    public float fadeSpeed;
    public float weakFactor;

    static GameController singleton;
    public static GameController Get() { return singleton; }

    Texture2D whiteText;
    float alpha = 0;
    Color currentColor;
    int initPickupCount, pickupCount;
    GameObject[] enemies;
    bool fading;
    Text textLabel;
    GameObject pauseGameobject, player;
	
    void Awake()
    {
        singleton = this;
	    if(Application.loadedLevel == 0)
		    OnLevelWasLoaded(0);
        whiteText = new Texture2D(1, 1);
        whiteText.SetPixel(1, 1, Color.white);
        whiteText.Apply();

        pauseGameobject = GameObject.Find("Menu");
        if(pauseGameobject)
            pauseGameobject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && pauseGameobject)
        {
            pauseGameobject.SetActive(!pauseGameobject.activeSelf);
            player.GetComponent<PlayerScript>().enabled = !player.GetComponent<PlayerScript>().enabled;
        }
    }

    void OnGUI()
    {
        if(alpha > 0)
        {
            GUI.depth = -1;
            GUI.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), whiteText);
        }
    }

    void OnLevelWasLoaded(int level)
    {
	    GameObject spawn = GameObject.FindGameObjectWithTag(spawnTag);
	    player = (GameObject)Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
        
        if (level != 0)
        {
            initPickupCount = pickupCount = GameObject.FindGameObjectsWithTag("Pickup").GetLength(0);
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject ui = (GameObject)Instantiate(uiPrefab);
            player.transform.GetChild(0).GetComponent<ProxyDetector>().SetSlider(ui.transform.GetChild(0).GetComponent<Slider>());
            textLabel = ui.transform.GetChild(1).GetComponent<Text>();
            textLabel.text = "Pickups left: " + pickupCount;
        }

        StartFadeIn(null, Color.black);
    }
    
    public void LoadLevel(int level)
    {
        StartFadeOut(() => Application.LoadLevel(level), Color.white);
    }

    public void StartFadeIn(Action action, Color fromColor)
    {
        if (fading)
            return;
        alpha = 1;
        currentColor = fromColor;
        StartCoroutine(FadeIn(action));
    }

    public void StartFadeOut(Action action, Color toColor)
    {
        if (fading)
            return;
        alpha = 0;
        currentColor = toColor;
        StartCoroutine(FadeOut(action));
    }

    IEnumerator FadeOut(Action action)
    {
        fading = true;
        while (alpha < 1)
        {
            alpha += fadeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (action != null)
            action();
        fading = false;
    }

    IEnumerator FadeIn(Action action)
    {
        fading = true;
        while (alpha > 0)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (action != null)
            action();
        fading = false;
    }

    public void Pickup()
    {
        pickupCount--;
        textLabel.text = "Pickups left: " + pickupCount;
        if (pickupCount == 0)
        {
            int index = UnityEngine.Random.Range(0, enemies.Length);
            enemies[index].GetComponent<Enemy>().Mark();
        }
    }

    public float GetWeakeningFactor()
    {
        return 1 + weakFactor * (initPickupCount - pickupCount);
    }

    public float GetPickupProgress()
    {
        return 1 - (float)pickupCount / initPickupCount;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
