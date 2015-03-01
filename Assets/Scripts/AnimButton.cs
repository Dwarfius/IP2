using UnityEngine;
using System.Collections;

public class AnimButton : MonoBehaviour {

	private Animator animator;
	GameObject Player;
	public string PlayerTag = "Player";
	public AnimDoorScript animScript;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.tag==PlayerTag)
		{
			Debug.Log ("Hello");
	  		animator.SetInteger("AnimState",1);
		}
		animScript.AnimateDoor();
}
}