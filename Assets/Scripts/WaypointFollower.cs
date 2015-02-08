using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class Waypoint
{
    public Transform target;
    public Transform lookAtTarget;
    public float waitTime;
}

public class WaypointFollower : MonoBehaviour 
{
    public Color targetColor = Color.red, lineColor = Color.yellow, lookAtColor = Color.green;
    public float gizmosRadius = 0.4f;
    public float speed = 1;
    public List<Waypoint> waypoints = new List<Waypoint>();

    IEnumerator wpEnum;

	void Start () 
    {
        wpEnum = waypoints.GetEnumerator();
        wpEnum.MoveNext();
	}

    void Update()
    {
        if(waypoints.Count > 0)
        {
            Waypoint w = wpEnum.Current as Waypoint;
            if (Vector3.Distance(transform.position, w.target.position) < 0.2f) //if we reached the waypoint
            {
                if (w.lookAtTarget)
                    transform.LookAt(w.lookAtTarget);
                MoveToNext(w.waitTime);
            }
            else //if not, then continue moving towards it
            {
                Vector3 dir = (w.target.position - transform.position).normalized;
                transform.Translate(dir * speed);
            }
        }
    }

    IEnumerator MoveToNext(float time)
    {
        yield return new WaitForSeconds(time);
        if (!wpEnum.MoveNext()) //if we reached the last waypoint
        {
            wpEnum.Reset(); //we get the first waypoint
            wpEnum.MoveNext();
        }
    }

    void OnDrawGizmos()
    {
        if(waypoints.Count > 1)
        {
            IEnumerator gizmosEnum = waypoints.GetEnumerator(); //using enumerators to optimaly traverse the list
            gizmosEnum.MoveNext();
            bool reachedEnd = false;
            while(!reachedEnd)
            {
                Waypoint wpCurrent = gizmosEnum.Current as Waypoint; //first of all, getting the current and the next waypoints to connect them
                if(!gizmosEnum.MoveNext())
                {
                    gizmosEnum.Reset();
                    gizmosEnum.MoveNext();
                    reachedEnd = true;
                }
                Waypoint wpNext = gizmosEnum.Current as Waypoint;

                Gizmos.color = targetColor; //first of all, drawing the waypoint
                Gizmos.DrawSphere(wpCurrent.target.position, gizmosRadius);
                Handles.Label(wpCurrent.target.position - wpCurrent.target.up * 0.1f, wpCurrent.target.name + "\nWait: " + wpCurrent.waitTime); //creating a label for easier understanding
                
                Gizmos.color = lineColor; //then, the line between the current and the next
                Gizmos.DrawLine(wpCurrent.target.position, wpNext.target.position);

                if(wpNext.lookAtTarget) //if we have a target to look at, draw it as well
                {
                    Gizmos.color = lookAtColor;
                    Gizmos.DrawSphere(wpCurrent.lookAtTarget.position, gizmosRadius / 2);
                    Gizmos.DrawLine(wpCurrent.target.position, wpCurrent.lookAtTarget.position);
                }
            }
        }
    }
}
