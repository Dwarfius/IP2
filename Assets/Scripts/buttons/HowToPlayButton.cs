using UnityEngine;
using System.Collections;

public class HowToPlayButton : MonoBehaviour {

	public Texture2D texture = null;
	public Texture2D hoverTexture;
	public Texture2D currentTexture;


	// Use this for initialization
	void Start () {

		currentTexture=texture;
	
	}

	IEnumerator PlayButtonClickHTP()
	{
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);
		Application.LoadLevel("HowToPLayScene");
	}

	private void OnGUI () {
		if(GUI.Button (new Rect(Screen.width /10 , Screen.height *5 /9 , texture.width, texture.height), currentTexture,GUIStyle.none)) {
			
			StartCoroutine(PlayButtonClickHTP());
		}
	}
	
	// Update is called once per frame
	void Update () {
		Rect boundingBox=new Rect(Screen.width /10 , Screen.height *5 /9 , currentTexture.width, currentTexture.height);
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
