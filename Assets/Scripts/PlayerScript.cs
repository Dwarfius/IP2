using UnityEngine;
using System.Collections;

//<<<<<<< HEAD 
/*
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
=======
*/
public class PlayerScript : MonoBehaviour 
{
    public float playerVelocity;
    public int pickupCount;
	public string PickupTag = "Pickup";
	public GameObject player;
	
    Animator animator;
    GameObject coin;
    bool messagePopup;
    Vector2 playerPosition;
    string labelText = "";

	void Start () 
    {
		animator = GetComponent<Animator>();
	}
	
	void Update () 
    {
        playerPosition = transform.position;
		playerPosition.x += Input.GetAxis("Horizontal") * playerVelocity * Time.deltaTime;
        playerPosition.y += Input.GetAxis("Vertical") * playerVelocity * Time.deltaTime;
        rigidbody2D.MovePosition(playerPosition);

		if (Input.GetKeyDown(KeyCode.Space) && coin)
		{
			Destroy(coin);
			messagePopup = false;
            pickupCount++;
		}
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == PickupTag)
		{
			messagePopup = true;
			labelText = "Press Space to pickup";
			coin = col.gameObject;
		}

        if (col.gameObject.tag == "Enemy")
            Application.LoadLevel(Application.loadedLevel);

		if (col.gameObject.name=="Button")
			animator.SetInteger("AnimState", 1);
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Barrel")
		{
			player.SetActive(true);
		}
	}
	void OnGUI()
	{
		if (messagePopup)
			GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,120), labelText);
//>>>>>>> origin/master
	}
}
