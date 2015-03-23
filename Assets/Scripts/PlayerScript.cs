using UnityEngine;
using System.Collections;
public class PlayerScript : MonoBehaviour 
{
    public float playerVelocity;
    public float cameraVelocity = 1;
    public float cameraTravelDist = 1;
	public string PickupTag = "Pickup";
    public string TeleportTag = "Teleport";
    public string EnemyTag = "Enemy";
	public GameObject player;
    public GameObject recievingSprite;
    public GameObject sendingSprite;
    //*
    public GameObject Key1;
    public GameObject Key2;
    public GameObject Key3;
    public string Key1Tag = "Key1";
    public string Key2Tag = "Key2";
    public string Key3Tag = "Key3";
    public string LockedDoor1Tag = "LockedDoor1";
    public string LockedDoor2Tag = "LockedDoor2";
    public string LockedDoor3Tag = "LockedDoor3";
    public bool key1PickedUp;
    public bool key2PickedUp;
    public bool key3PickedUp;
    public bool collisionWithLockedDoor1;
    public bool collisionWithLockedDoor2;
    public bool collisionWithLockedDoor3;
    //*

    [HideInInspector] public bool cameraControl;
	
    Animator animator;
    GameObject pickup;
    Enemy enemy;
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

        if (Input.GetKeyDown(KeyCode.Space) && pickup)
        {
            Destroy(pickup);
            messagePopup = false;
            GameController.Get().Pickup();
        }

        if(Input.GetKeyDown(KeyCode.Space) && enemy && enemy.marked)
        {
            Destroy(enemy.gameObject);
            GameController.Get().StartFadeOut(() => Application.LoadLevel(Application.loadedLevel + 1), Color.white);
        }

        if (dy > 0)
            animator.SetInteger("AnimState", 1);
        else if (dy < 0)
            animator.SetInteger("AnimState", 0);
        else if (dx > 0)
            animator.SetInteger("AnimState", 2);
        else if (dx < 0)
            animator.SetInteger("AnimState", 3);
        else if (dx == 0 && dy == 0)
            animator.SetInteger("AnimState", -1);

        //*
        if (key1PickedUp == true && collisionWithLockedDoor1 )
        {

        }
        //*

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == PickupTag)
            pickup = col.gameObject;
    
        //Teleportation - this part of the script teleports the player from the selected position to the new position
        if (col.tag == TeleportTag)
            player.transform.position=recievingSprite.transform.position;

        if(col.tag == EnemyTag)
            enemy = col.GetComponent<Enemy>();

        //*

        if (col.tag == Key1Tag)
        {
            Debug.Log("Key 1 Picked Up");
            Destroy(Key1.gameObject);
            key1PickedUp = true;
            //this is where the code goes for the key to be displayed
        }

        if (col.tag == LockedDoor1Tag)
        {
            collisionWithLockedDoor1 = true;
        }

        if (col.tag == LockedDoor2Tag)
        {
            collisionWithLockedDoor2 = true;
        }

        if (col.tag == LockedDoor3Tag)
        {
            collisionWithLockedDoor3 = true;
        }

        //*
    }
        
	void OnTriggerExit2D(Collider2D col)
	{
        if (col.tag == PickupTag)
            pickup = null;
        else if (col.tag == EnemyTag)
            enemy = null;
		else if (col.tag == "Barrel")
			player.SetActive(true);
	}
	void OnGUI()
	{
		if (messagePopup)
			GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,200), labelText);
	}

}
