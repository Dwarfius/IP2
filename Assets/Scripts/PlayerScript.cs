using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float playerVelocity;
    public float cameraVelocity = 1;
    public float cameraTravelDist = 1;
    public string PickupTag = "Pickup";
   
    public string EnemyTag = "Enemy"; 
    public GameObject player;


    public string Key1Tag = "Key1";
    public string Door1Tag = "LockedDoor1";
    
    public AudioClip keyPickup;
    public AudioClip pickUp;
    public AudioClip enemyDefeat;
    public AudioClip heavenGates;
    public AudioClip footSteps;
    bool keyPopUpMessage;
    float keyMessagePopTime = 5.0F;

    [HideInInspector]
    public bool cameraControl;

    Animator animator;
    GameObject pickup;
    Enemy enemy;
    bool keyPopup, key1Collected;
    string keyText = "";
    Transform cameraTrans;

    void Start()
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
            Vector2 playerPosition = transform.position;
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

        //If the player presses space and is touching a pick up then destroy it, play audio and set keyPop up to false
        if (Input.GetKeyDown(KeyCode.Space) && pickup)
        {
            Destroy(pickup);
            audio.PlayOneShot(pickUp);
            keyPopup = false;
            GameController.Get().Pickup();
        }

        if (Input.GetKeyDown(KeyCode.Space) && enemy && enemy.marked)
        {
            Destroy(enemy.gameObject);
            audio.PlayOneShot(enemyDefeat);
            GameController.Get().StartFadeOut(() => Application.LoadLevel(Application.loadedLevel + 1), Color.white);
            audio.PlayOneShot(heavenGates);
        }
        //This part animates the players movements 
        //UP
        if (dy > 0)
            animator.SetInteger("AnimState", 1);
            //DOWN
        else if (dy < 0)
            animator.SetInteger("AnimState", 0);
            //RIGHT
        else if (dx > 0)
            animator.SetInteger("AnimState", 2);
            //LEFT
        else if (dx < 0)
            animator.SetInteger("AnimState", 3);
            //IDLE
        else if (dx == 0 && dy == 0)
            animator.SetInteger("AnimState", -1);
            
        //If the player is moving in the x-axis and there is no audio currently playing then play the foot step audio
        if (dx !=0 && !audio.isPlaying)
        {
            audio.clip = footSteps;
            audio.Play();
            
        }
        //If the player is moving in the y-axis and there is no audio currently playing then play the foot step audio
        if (dy != 0&& !audio.isPlaying)
        {
            audio.clip = footSteps;
            audio.Play();
            
        }
     
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //this stores pickup as a gameObject so that it can be used in the update function
        if (col.tag == PickupTag)
            pickup = col.gameObject;
            
       

        if (col.tag == EnemyTag)
        {
            enemy = col.GetComponent<Enemy>();
            if(!enemy.marked)
                GameController.Get().StartFadeOut(() => Application.LoadLevel(Application.loadedLevel), Color.red);
        }

        //if player collides with the key; destroy key, display pop up message, start timer and set key1Collected to true
        //so that we know the player does indeed have the key
        if (col.tag == Key1Tag)
        {
            Destroy(col.gameObject);
            key1Collected = true;
            keyText = "This key opens the yellow door!";
            audio.PlayOneShot(keyPickup);
            keyPopUpMessage = true;
            StartCoroutine(KeyMessageTimer());
        }
        //gets the unity event that unlocks the door, if the player has the key, if not then it displays a message telling the
        //player that they need the key to go through the door
        if (col.tag == Door1Tag)
        {
            if(key1Collected)
            {
                UnlockDoorScript unlockDoorScript = col.GetComponent<UnlockDoorScript>();
                unlockDoorScript.UnlockTheDoor();
            }
            else
            {
                keyText = "You cannot get through this door without the key!";
                keyPopUpMessage = true;
                StartCoroutine(KeyMessageTimer());
            }
                
        }
    }

    /// <summary>
    /// This is the timer for the key pop up message
    /// </summary>
    /// <returns></returns>
    IEnumerator KeyMessageTimer()
    {
        yield return new WaitForSeconds(keyMessagePopTime);
        keyPopUpMessage = false;
    }
    /// <summary>
    /// This part is what happens when the player exits certain objects, for example when the player 
    /// exits the barrel we set player to true, so that the players renderer and the collider2D comes back
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == PickupTag)
            pickup = null;
        else if (col.tag == EnemyTag)
            enemy = null;
        else if (col.tag == "Barrel")
            player.SetActive(true);
    }

    /// <summary>
    /// Displays messages:
    /// 1 - for when the player collects the key
    /// 2 - for when the player touches the door without the key
    /// </summary>
    void OnGUI()
    {
        if (keyPopup)
            GUI.Box(new Rect(140, Screen.height - 50, Screen.width - 300, 200), keyText);

        if (keyPopUpMessage)
            GUI.Box(new Rect(140, Screen.height - 50, Screen.width - 300, 200), keyText);
    }
}

