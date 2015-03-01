using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {


	//variable to store the pop up message when player hits a pick up
	public bool messagePopup;
	//variable which stores the text that is contained in the messagePopup
	public string labelText = "";
	//variable which declares the coin game object
	GameObject box;
	public string PlayerTag = "Player";
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.Space) && box	)
		{
			Destroy(gameObject);
			messagePopup = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log (col.gameObject.name);
		if (col.gameObject.tag == PlayerTag)
		{
			
			messagePopup = true;
			labelText = "Press Space to pickup";
			box = col.gameObject;
		}
	}

	void OnGUI()
		
	{
		if (messagePopup)
		{
			GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
		}
	}
}
