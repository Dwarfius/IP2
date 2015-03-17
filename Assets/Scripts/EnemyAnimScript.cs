using UnityEngine;
using System.Collections;

public class EnemyAnimScript : MonoBehaviour {

    public float angle;
    Animator animator;
	// Use this for initialization
	void Start () 
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        angle = transform.rotation.z;

        if (angle > 0)
        {
            animator.SetInteger("AnimState", 1);
        }
        else if (angle < 0)
        {
            animator.SetInteger("AnimState", 0);
        }
        else if (angle > 0)
        {
            animator.SetInteger("AnimState", 2);
        }
        else if (angle < 0)
        {
            animator.SetInteger("AnimState", 3);
        }
	}
}
