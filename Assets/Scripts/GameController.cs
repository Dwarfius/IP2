using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GameObject playerPrefab;
    public string spawnTag;
	//
	public float playerVelocity;
	public Vector2 playerPosition;
	public float boundary;
	public bool messagePopup;
	public string labelText = "";





    void Start()
    {
		playerPosition=gameObject.transform.position;
        DontDestroyOnLoad(gameObject);
    }

	void Update()
	{
		playerPosition.x += Input.GetAxis("Horizontal")*playerVelocity;
		playerPosition.y += Input.GetAxis("Vertical")*playerVelocity;
		 
		transform.position = playerPosition;

		if (playerPosition.x <- boundary)
		{
			transform.position = new Vector2(-boundary,playerPosition.y);
		}
		if (playerPosition.x > boundary)
		{
			transform.position = new Vector2(boundary,playerPosition.y);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{

		if (col.gameObject.tag=="Pickup")
		{

			messagePopup = true;
			labelText = "Press Space to pickup";

		}

		if (col.gameObject.tag=="Pickup" && Input.GetKeyDown(KeyCode.Space))
		{
			Destroy(gameObject);
		}

	
	}


	void OnGUI()
	
	{
		if (messagePopup)
		{
			GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
		}

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
