using UnityEngine;
using System.Collections;

public class HidingScript : MonoBehaviour {

	public string PlayerTag = "Player";
	public GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag==PlayerTag)
		{
			Debug.Log ("Collision");

			player.renderer.enabled = false;
			collider2D.enabled = true;

		}


	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag==PlayerTag)
		{
			player.renderer.enabled = true;

	}

}
}