using UnityEngine;
using System.Collections;

public class HardButton : MonoBehaviour {

	public Texture2D texture = null;

	private void OnGUI () {
		if(GUI.Button (new Rect(Screen.width /8 , Screen.height *7 /11 , texture.width, texture.height), texture, GUIStyle.none)) {

			Application.LoadLevel("HardlLevel");
		}
	}
}