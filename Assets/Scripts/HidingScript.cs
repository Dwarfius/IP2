using UnityEngine;
using System.Collections;

public class HidingScript : MonoBehaviour {

	public string PlayerTag = "Player";
	public GameObject player;
	GameObject playerHidden;
    public bool isPlayerHidden;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I) && isPlayerHidden)
		{
			player.renderer.enabled = false;
			player.collider2D.enabled = false;
		}

	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag==PlayerTag)
		{
			Debug.Log ("Collision");


			playerHidden = col.gameObject;
            isPlayerHidden = true;

		}


	}
   
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag==PlayerTag)
		{
            isPlayerHidden = false;
            player.renderer.enabled = false;
			player.collider2D.enabled = false;

	    }
    }
  
}