using UnityEngine;
using System.Collections;

public class StartSceneScript : MonoBehaviour {

	//variable for background texture
	public Texture2D backGroundTexture;
	//variable for the width of the buttons
	public int buttonWidth = 100;
	//variable for the height of the button
	public int buttonHeight = 30;
	//variable for the spacing between the buttons 
	public int buttonSpacing = 70;
	//variable for the start position of the buttons
	public int buttonYStart = 300;


	//This method provides a declaration so the background image can be applied, it then declares the buttons on the screen.
	void OnGUI() {

		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backGroundTexture);
		int buttonYPosition = buttonYStart;
		if (GUI.Button (new Rect(Screen.width/2-buttonWidth/2,buttonYPosition,buttonWidth,buttonHeight),"Start"));

		{
			Application.LoadLevel ("");

		}

		buttonYPosition+=buttonSpacing;

		if (GUI.Button (new Rect(Screen.width/2-buttonWidth/2,buttonYPosition,buttonWidth,buttonHeight),"Options Menu"));

		{
			Application.LoadLevel ("");
		}

		buttonYPosition+=buttonSpacing;

		if (GUI.Button (new Rect(Screen.width/2-buttonWidth/2,buttonYPosition,buttonWidth,buttonHeight),"Quit"));

		{
			Application.Quit();
		}




	}
	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
