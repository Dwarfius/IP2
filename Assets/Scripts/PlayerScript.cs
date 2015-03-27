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

        public string Key1Tag = "Key1";
        public string Door1Tag = "LockedDoor1";
        public bool key1Collected;
        public GameObject Key1;
        GameObject collisionWithDoor1;
        public UnlockDoorScript unlockDoorScript;
        public AudioClip keyPickup;
        public AudioClip pickUp;
        public AudioClip doorOpenSound;

        
        //public AudioClip footSteps;
        

        

        [HideInInspector]
        public bool cameraControl;

        Animator animator;
        GameObject pickup;
        Enemy enemy;
        bool messagePopup;
        Vector2 playerPosition;
        string labelText = "";
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

            if (Input.GetKeyDown(KeyCode.Space) && enemy && enemy.marked)
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
            
            if (Input.GetKeyDown(KeyCode.O) && key1Collected && collisionWithDoor1)
            {
                unlockDoorScript.UnlockTheDoor();

            }
            /*
            if (dy != 0 || dx !=0)
            {
                audio.Play();
            }
             * */
            

        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == PickupTag)
                pickup = col.gameObject;

            //Teleportation - this part of the script teleports the player from the selected position to the new position
            if (col.tag == TeleportTag)
                player.transform.position = recievingSprite.transform.position;

            if (col.tag == EnemyTag)
                enemy = col.GetComponent<Enemy>();

            if (col.tag == Key1Tag)
            {
                Debug.Log("Object picked up");
                Destroy(Key1.gameObject);
                key1Collected = true;
                audio.PlayOneShot(keyPickup);
            }

            if (col.tag == Door1Tag)
            {
                collisionWithDoor1 = col.gameObject;
            }



        }


        void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag == PickupTag)
                pickup = null;
            else if (col.tag == EnemyTag)
                enemy = null;
            else if (col.tag == "Barrel")
                player.SetActive(true);

          if (col.tag == Door1Tag)
          {
              collisionWithDoor1 = null;
          }
        }
        void OnGUI()
        {
            if (messagePopup)
                GUI.Box(new Rect(140, Screen.height - 50, Screen.width - 300, 200), labelText);
        }

    }

