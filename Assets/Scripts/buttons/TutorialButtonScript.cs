using UnityEngine;
using System.Collections;

public class TutorialButtonScript : MonoBehaviour {

	public Texture2D texture = null;
	public Texture2D hoverTexture;
	public Texture2D currentTexture;

	public void Start()
	{
		currentTexture=texture;
	}

	IEnumerator PlayButtonClickCredits()
	{
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);
		Application.LoadLevel("TutorialLevel");
	}
	
	private void OnGUI () {
		if(GUI.Button (new Rect(Screen.width /8 , Screen.height *5 /11 , texture.width, texture.height), currentTexture,GUIStyle.none)) {
			
			StartCoroutine(PlayButtonClickCredits());
			
			
		}
	}
	public void Update() {
		Rect boundingBox=new Rect(Screen.width /8 , Screen.height *5 /11 , currentTexture.width, currentTexture.height);
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
