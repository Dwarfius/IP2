using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {

	public string PlayerTag = "Player";
	public GameObject Player;
	public bool messagePopup;
	//variable which stores the text that is contained in the messagePopup
	public string labelText = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerEnter2D (Collider2D col)
	{
		
		if (col.gameObject.tag == PlayerTag)
		{
			messagePopup = true;
			labelText = "Collect all items in order to make your enemies forget about you.  Once you have, you can walk through them and you will be " +
				"forgotten";

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

