using UnityEngine;
using System.Collections;
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
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        playerPosition = transform.position;
		playerPosition.x += dx * playerVelocity * Time.deltaTime;
        playerPosition.y += dy * playerVelocity * Time.deltaTime;
        rigidbody2D.MovePosition(playerPosition);

		if (Input.GetKeyDown(KeyCode.Space) && coin)
		{
			Destroy(coin);
			messagePopup = false;
            pickupCount++;
		}
     
        if (dx!=0 || dy!=0)
        {
            player.renderer.enabled = true;
            player.collider2D.enabled = true;
        }
         
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == PickupTag)
		{
			messagePopup = true;
			labelText = "These are pick up objects. They are used to make others forget\n"	
				+"about you which is something you must do in order to obtain your goal.\n" +
					"Note that everytime you pick one of these up the guage above the enemy will decrease\n" +
					"Once this guage is below a certain level it will turn from a green colour to a red colour. You can only pass through enemies to complete\n" +
					"the level if their guage is red.  To pass through an enemy simply go behind an enemy and press i\n";
			coin = col.gameObject;
		}

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
			GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,200), labelText);
	}
}
