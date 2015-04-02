using UnityEngine;
using System.Collections;

public class RestartButton : MonoBehaviour {

	public Texture2D texture = null;
	public Texture2D hoverTexture;
	public Texture2D currentTexture;

	// Use this for initialization
	void Start () {

		currentTexture=texture;
	
	}

	// Update is called once per frame
	private void OnGUI (){

		if(GUI.Button (new Rect(Screen.width /2 - currentTexture.width/2 , Screen.height *3 /5 , texture.width, texture.height), currentTexture, GUIStyle.none)) {
			Application.LoadLevel("testScene");
		}
	}

	public void Update() {
		Rect boundingBox=new Rect(Screen.width /3 , Screen.height *3 /5 , currentTexture.width, currentTexture.height);
		Vector3 actualMousePosition = Input.mousePosition;
		actualMousePosition.y = Screen.height-Input.mousePosition.y;
		if (boundingBox.Contains(actualMousePosition)){
			//Input.mousePosition
			currentTexture=hoverTexture;
		}
		else
		{
			currentTexture=texture;
		}
	}
}
