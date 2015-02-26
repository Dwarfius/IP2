using UnityEngine;
using System.Collections;

public class AnimPlayer : MonoBehaviour {
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal")<0){
			animator.SetInteger("AnimState",3);
		}

		if(Input.GetAxis("Horizontal")>0){
			animator.SetInteger("AnimState",2);
		}
		if(Input.GetAxis("Vertical")<0){
			animator.SetInteger("AnimState",0);
		}
		if(Input.GetAxis("Vertical")>0){
			animator.SetInteger("AnimState",1);
		}
	}
}
