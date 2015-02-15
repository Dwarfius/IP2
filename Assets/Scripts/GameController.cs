using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GameObject playerPrefab;
    public string spawnTag;
	//variable for the speed of the player movement
	public float playerVelocity;
	//variable for the position of the player
	public Vector2 playerPosition;
	//variable for the boundary to prevent the player going out of the game
	public float boundary;
	//variable to store the pop up message when player hits a pick up
	public bool messagePopup;
	//variable which stores the text that is contained in the messagePopup
	public string labelText = "";
	//variable which declares the coin game object
	public GameObject coin;
	//variable which declares the Player game object
	public GameObject Player;
	//variable which declares the players health, with a start value of 100
	public int health = 100;





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

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Destroy(coin);
			messagePopup = false;
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

		if (col.gameObject.tag=="Enemy")
		{
			health -=10;
		}
	
	}


	void OnGUI()
	
	{
		if (messagePopup)
		{
			GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
		}

		GUI.Label (new Rect(20,20,200,80), "Health: "+health.ToString());

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
