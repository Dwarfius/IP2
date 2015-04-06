using UnityEngine;
using System.Collections;

public class HidingScript : MonoBehaviour 
{
	public string PlayerTag = "Player";
    
    GameObject player;
   
    public AudioSource hideSound;
    private Animator animator;

    /// <summary>
    /// This method gets the Animator component and sets it to animator so that it can be used in this script to animate the barrel.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// This method works out whether or not the players rednerer/collider2D is enabled, if so then it is disabled when the player
    /// presses H and is touching the barrel, it also gives the player the ability to pan the camera about in order to "plan ahead"
    /// It also animates the barrel sptie and plays an audio clip
    /// </summary>
	void Update () 
    {
		if (Input.GetKeyDown(KeyCode.H) && player)
		{
            player.renderer.enabled = !player.renderer.enabled;
            player.collider2D.enabled = !player.collider2D.enabled;
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            playerScript.cameraControl = !playerScript.cameraControl;
            if (player.renderer.enabled)
            {
                player.transform.GetChild(0).localPosition = new Vector3(0, 0, -10);
                player = null;
            }
            animator.SetInteger("AnimState", 1);
            audio.Play();

            
		}
	}
    /// <summary>
    /// This method sets the player to a gameObject so that we can use it in the update method
    /// </summary>
    /// <param name="col"></param>
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag==PlayerTag)
            player = col.gameObject;
       
            
	}

    /// <summary>
    /// This method sets the player to null and animates the barrel sprite to the original one when the player leaves the barrel
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == PlayerTag)
            player = null;
        animator.SetInteger("AnimState", 0);
    }
}