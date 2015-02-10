using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GameObject playerPrefab;
    public string spawnTag;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded(int level)
    {
        if(level != 0)
        {
            GameObject spawn = GameObject.FindGameObjectWithTag(spawnTag);
            GameObject.Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
        }
    }
    
    public void LoadLevel(int i)
    {
        Application.LoadLevel(i);
    }
}
