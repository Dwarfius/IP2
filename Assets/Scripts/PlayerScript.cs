using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

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
	GameObject coin;
	//variable which declares the Player game object
	public GameObject Player;
	//variable which declares the players health, with a start value of 100
	//public int health = 100;
	public GameObject Enemy;
	public string PickupTag = "Pickup";
	private Animator animator;
	 

	public int pickupCount;
	GameObject enemy;

	GameObject Button;

	// Use this for initialization
	void Start () {

		playerPosition=gameObject.transform.position;
		DontDestroyOnLoad(gameObject);
		//.
		animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

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

		if (Input.GetKeyDown(KeyCode.Space) && coin)
		{
			Destroy(coin);
			messagePopup = false;
		}
		pickupCount++;

		if (pickupCount == 3 && enemy)
		{
			Destroy(enemy);
		}
	}

		

	
	void OnTriggerEnter2D (Collider2D col)
	{
		Debug.Log (col.gameObject.name);
		if (col.gameObject.tag == PickupTag)
		{
			
			messagePopup = true;
			labelText = "Press Space to pickup";
			coin = col.gameObject;

			
		}


		if (col.gameObject.name=="Cone")
		{
			Application.Quit();
		}
		/*
		if (col.gameObject.tag == enemy)
		{
			enemy = col.gameObject;
		}
		*/

		if (col.gameObject.name=="Button")
		{
			animator.SetInteger("AnimState",1);
		}
	
	}

	void OnGUI()
		
	{
		if (messagePopup)
		{
			GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
		}
		
		//GUI.Label (new Rect(20,20,200,80), "Health: "+health.ToString());
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{

	}
}
