using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    [HideInInspector] public bool marked;

    WaypointFollower wpFollower;
    Animator animator;

    void Start()
    {
        wpFollower = GetComponent<WaypointFollower>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float angle = Mathf.Atan2(wpFollower.dir.y, wpFollower.dir.x) * Mathf.Rad2Deg;
        if (45 >= angle && angle >= -45) //taking the usual counter clockwise indexing
            animator.SetInteger("Direction", marked ? 4 : 0);
        else if (135 >= angle && angle >= 45)
            animator.SetInteger("Direction", marked ? 5 : 1);
        else if (angle >= 135 || angle <= -135)
            animator.SetInteger("Direction", marked ? 6 : 2);
        else
            animator.SetInteger("Direction", marked ? 7 : 3);
    }

    public void Mark()
    {
        Debug.Log(gameObject.name);
        marked = true;
    }
}
