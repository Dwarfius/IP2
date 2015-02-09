using UnityEngine;
using System.Collections;

public class OptionsMenuScript : MonoBehaviour {

	//variable for background texturre
	public Texture2D backGroundTexture;
	//variable for the width of the buttons
	public int buttonWidth = 100;
	//variable for the height of the button
	public int buttonHeight = 30;
	//variable for the spaces between the buttons
	public int buttonSpacing = 70;
	public int buttonYBack = 300;


	/*
	 * This method declarres the background texture and the buttons on the screen
	 * 
	 * */
	void OnGUI() {

		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backGroundTexture);

		int buttonYPosition = buttonYBack;

		if (GUI.Button(new Rect(Screen.width/2-buttonWidth/2,buttonYPosition,buttonWidth,buttonHeight),"Back"))

		{

			Application.LoadLevel("StartScene");

		}


	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
