using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
    public float playerVelocity;
    public int pickupCount;
	public string PickupTag = "Pickup";
	
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

	void OnGUI()
	{
		if (messagePopup)
			GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,120), labelText);
	}
}
