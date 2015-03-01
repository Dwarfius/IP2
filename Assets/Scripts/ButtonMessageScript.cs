using UnityEngine;
using System.Collections;

public class ButtonMessageScript : MonoBehaviour {
	
	private Animator animator;
	GameObject Player;
	public string PlayerTag = "Player";
	//variable to store the pop up message when player hits a pick up
	public bool messagePopup;
	//variable which stores the text that is contained in the messagePopup
	public string labelText = "";
	public int messageTime = 2;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{

			messagePopup = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		
		if (col.gameObject.tag==PlayerTag)
		{
			Debug.Log ("Hello");
			animator.SetInteger("AnimState",1);
		}

		if (col.gameObject.tag == PlayerTag)
		{
			messagePopup = true;
			labelText = "This will be the tutorial message.  Press K to delete this message.";
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		animator.SetInteger("AnimState",0);
	}

	void OnGUI()
		
	{
		if (messagePopup)
		{
			GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
		}
	}
}