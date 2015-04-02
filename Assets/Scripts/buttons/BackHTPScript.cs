using UnityEngine;
using System.Collections;

public class BackHTPScript : MonoBehaviour {

	public Texture2D texture = null;
	public Texture2D hoverTexture;
	public Texture2D currentTexture;
	
	public void Start()
	{
		currentTexture=texture;
	}
	
	private void OnGUI () {
		if(GUI.Button (new Rect(Screen.width *6/7, Screen.height *3 /23, texture.width, texture.height), currentTexture, GUIStyle.none)) {
			
			Application.LoadLevel("TutorialLevel");
			
			
		}
	}
	public void Update() {
		Rect boundingBox=new Rect(Screen.width *6/7 , Screen.height *3 /23 , currentTexture.width, currentTexture.height);
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
