using UnityEngine;
using System.Collections;

public class EnemyAnimScript : MonoBehaviour {

    Animator animator;
	// Use this for initialization
	void Start () 
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float ex = Input.GetAxis("Horizontal");
        float ey = Input.GetAxis("Vertical");


        if (ey > 0)
        {
            animator.SetInteger("AnimState", 1);
        }
        else if (ey < 0)
        {
            animator.SetInteger("AnimState", 0);
        }
        else if (ex > 0)
        {
            animator.SetInteger("AnimState", 2);
        }
        else if (ex < 0)
        {
            animator.SetInteger("AnimState", 3);
        }
	}
}
