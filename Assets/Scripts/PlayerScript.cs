using UnityEngine;
using System.Collections;
public class PlayerScript : MonoBehaviour 
{
    public float playerVelocity;
    public float cameraVelocity = 1;
    public float cameraTravelDist = 1;
    public int pickupCount;
	public string PickupTag = "Pickup";
    public string TeleportTag = "Teleport";
	public GameObject player;
    public GameObject recievingSprite;
    public GameObject sendingSprite;

    [HideInInspector] public bool cameraControl;
	
    Animator animator;
    GameObject coin;
    bool messagePopup;
    Vector2 playerPosition;
    string labelText = "";
    Transform cameraTrans;
 
	void Start () 
    {
		animator = GetComponent<Animator>();
        cameraTrans = transform.GetChild(0);
        
	}

    void Update()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        if (!cameraControl)
        {
            playerPosition = transform.position;
            playerPosition.x += dx * playerVelocity * Time.deltaTime;
            playerPosition.y += dy * playerVelocity * Time.deltaTime;
            rigidbody2D.MovePosition(playerPosition);
            if (dx != 1 && dy != 1)
                rigidbody2D.velocity = new Vector2(0, 0);
        }
        else
        {
            Vector2 offset = new Vector2(cameraVelocity * dx * Time.deltaTime, cameraVelocity * dy * Time.deltaTime);
            Vector2 pos = cameraTrans.localPosition;
            if ((pos + offset).magnitude < cameraTravelDist)
                pos += offset;
            cameraTrans.localPosition = new Vector3(pos.x, pos.y, -10);
        }

        if (Input.GetKeyDown(KeyCode.Space) && coin)
        {
            Destroy(coin);
            messagePopup = false;
            pickupCount++;
        }

        if (dy > 0)
        {
            animator.SetInteger("AnimState", 1);
            
        }
        else if (dy < 0)
        {
            animator.SetInteger("AnimState", 0);
            
        }
        else if (dx > 0)
        {
            animator.SetInteger("AnimState", 2);
            
        }
        else if (dx < 0)
        {
            animator.SetInteger("AnimState", 3);

        }
        else if (dx == 0 && dy == 0)
        {
            animator.SetInteger("AnimState", -1);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == PickupTag)
        {
            messagePopup = true;
            labelText = "These are pick up objects. They are used to make others forget\n"
                + "about you which is something you must do in order to obtain your goal.\n" +
                    "Note that everytime you pick one of these up the guage above the enemy will decrease\n" +
                    "Once this guage is below a certain level it will turn from a green colour to a red colour. You can only pass through enemies to complete\n" +
                    "the level if their guage is red.  To pass through an enemy simply go behind an enemy and press i\n";
            coin = col.gameObject;
        }
    
        //Teleportation - this part of the script teleports the player from the selected position to the new position
        if (col.gameObject.tag == TeleportTag)
        {
            player.transform.position=recievingSprite.transform.position;
        }
}
        
		//if (col.gameObject.name=="Button")
			//animator.SetInteger("AnimState", 1);
	
        
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
