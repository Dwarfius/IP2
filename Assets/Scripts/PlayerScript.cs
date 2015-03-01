using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//variable for the speed of the player movement
	public float playerVelocity;
	//variable for the position of the player
	public Vector2 playerPosition;
	//variable which declares the Player game object
	public GameObject Player;
	//variable which declares the Enemy game object
	public GameObject Enemy;
	public string enemyTag = "Enemy";


	// Use this for initialization
	void Start () {

		playerPosition=gameObject.transform.position;
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

		playerPosition.x += Input.GetAxis("Horizontal")*playerVelocity;
		playerPosition.y += Input.GetAxis("Vertical")*playerVelocity;
		
		transform.position = playerPosition;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag==enemyTag	)
		{
			Debug.Log("Enemy destroyed");
			Destroy (Enemy);
		}
	}
}
