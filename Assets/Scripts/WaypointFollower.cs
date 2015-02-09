using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
    public float gizmosRadius = 0.15f;
    public float speed = 1;
    public List<Waypoint> waypoints = new List<Waypoint>();

    IEnumerator wpEnum;
    bool waiting;

	void Start () 
    {
        wpEnum = waypoints.GetEnumerator();
        if (waypoints.Count > 1)
        {
            wpEnum.MoveNext();
            transform.position = (wpEnum.Current as Waypoint).target.position;
            wpEnum.MoveNext();
            LookAt2D((wpEnum.Current as Waypoint).target);
        }
        else 
            Debug.LogError("There must be at least 2 waypoints defined for the component to work.");
	}

    void Update()
    {
        if(waypoints.Count > 1 && !waiting)
        {
            Waypoint w = wpEnum.Current as Waypoint;
            if (Vector3.Distance(transform.position, w.target.position) < 0.2f) //if we reached the waypoint
            {
                if (w.lookAtTarget)
                    LookAt2D(w.lookAtTarget);
                StartCoroutine(MoveToNext(w.waitTime));
            }
            else //if not, then continue moving towards it
            {
                Vector3 dir = (w.target.position - transform.position).normalized;
                transform.Translate(dir * speed * Time.deltaTime, Space.World);
            }
        }
    }

    IEnumerator MoveToNext(float time)
    {
        waiting = true;
        yield return new WaitForSeconds(time);
        waiting = false;
        if (!wpEnum.MoveNext()) //if we reached the last waypoint
        {
            wpEnum.Reset(); //we get the first waypoint
            wpEnum.MoveNext();
        }
        LookAt2D((wpEnum.Current as Waypoint).target);
    }

#if UNITY_EDITOR
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

                if (!wpCurrent.target || !wpNext.target) //safeguard for empty waypoints
                    return;

                Gizmos.color = targetColor; //first of all, drawing the waypoint
                Gizmos.DrawSphere(wpCurrent.target.position, gizmosRadius);
                Handles.Label(wpCurrent.target.position - wpCurrent.target.up * 0.1f, wpCurrent.target.name + "\nWait: " + wpCurrent.waitTime); //creating a label for easier understanding
                
                Gizmos.color = lineColor; //then, the line between the current and the next
                Gizmos.DrawLine(wpCurrent.target.position, wpNext.target.position);

                if(wpCurrent.lookAtTarget) //if we have a target to look at, draw it as well
                {
                    Gizmos.color = lookAtColor;
                    Gizmos.DrawSphere(wpCurrent.lookAtTarget.position, gizmosRadius / 2);
                    Handles.Label(wpCurrent.lookAtTarget.position - wpCurrent.target.up * 0.1f, wpCurrent.lookAtTarget.name); //creating a label for easier understanding
                    Gizmos.DrawLine(wpCurrent.target.position, wpCurrent.lookAtTarget.position);
                }
            }
        }
    }
#endif
    void LookAt2D(Transform target)
    {
        Vector3 n = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(n.y, n.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
