using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	//boolean variable for paused, "if game is paused..."
	private bool paused = false;
	//declares the space bar as the pause button
	public KeyCode Pause = KeyCode.Space;
	//variable for the width of the buttons 
	public int buttonWidth = 100;
	//variable for the height of the buttons 
	public int buttonHeight = 30;
	//variable for the spacing between the buttons
	public int buttonSpacing = 70;
	//variable for the start position of the buttons
	public int buttonYStart = 400;

	// Use this for initialization
	void Start () {
	
	}

	/*
	 * 
	 * This method checks once per frame to see if the player has paused the game, if it has then it also 
	 * stops the timer (if used)
	 * 
	 **/
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Space))
		{
			paused = !paused;
		}
		/*
		if (paused) 
		{
			Time.timeScale = 0;
		}
		else 
		{
			Time.timeScale = 1;
		}
	**/
	}

	void OnGUI()
	{
		if (paused)
		{
			int buttonYPosition = buttonYStart;

			if (GUI.Button (new Rect(Screen.width/2-buttonWidth/2,buttonYPosition,buttonWidth,buttonHeight),"Restart"))
			{
				Application.LoadLevel("");
			}

			buttonYPosition+=buttonSpacing;

			if (GUI.Button (new Rect(Screen.width/2-buttonWidth/2,buttonYPosition,buttonWidth,buttonHeight),"StartScene"))
			{
				Application.LoadLevel("StartScene");
			}

		}
	}
}



