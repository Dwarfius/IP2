using UnityEngine;
using System.Collections;

public class HidingScript : MonoBehaviour 
{
	public string PlayerTag = "Player";
    
    GameObject player;
   // public AudioClip hideSound;
    public AudioSource hideSound;

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
            audio.Play();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag==PlayerTag)
            player = col.gameObject;
            
	}

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == PlayerTag)
            player = null;
    }
}