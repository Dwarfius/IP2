using UnityEngine;
using System.Collections;

public class HidingScript : MonoBehaviour {

	public string PlayerTag = "Player";
	public GameObject player;
	GameObject playerHidden;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I) && playerHidden)
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

		}


	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag==PlayerTag)
		{
			player.renderer.enabled = true;
            player.collider2D.enabled = true;
	    }
    }
}