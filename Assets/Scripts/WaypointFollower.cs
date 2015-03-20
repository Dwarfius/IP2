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
    public float gizmosRadius = 0.04f;
    public float speed = 1;
    public bool mirrored;
    public List<Waypoint> waypoints = new List<Waypoint>();

    [HideInInspector] public Vector2 dir;

    int wpInd, delta = 1;
    bool waiting;
    Transform camTrans;

	void Start () 
    {
        camTrans = transform.GetChild(0);
        if (waypoints.Count > 1)
        {
            transform.position = waypoints[0].target.position;
            LookAt2D(waypoints[1].target);
        }
        else 
            Debug.LogError("There must be at least 2 waypoints defined for the component to work.");
	}

    void Update()
    {
        if(waypoints.Count > 1 && !waiting)
        {
            Vector2 myPos = transform.position;
            Waypoint w = waypoints[wpInd];
            Vector2 wPos = w.target.position;
            if (Vector2.Distance(myPos, wPos) < 0.01f) //if we reached the waypoint
            {
                if (w.lookAtTarget)
                    LookAt2D(w.lookAtTarget);
                StartCoroutine(MoveToNext(w.waitTime));
            }
            else //if not, then continue moving towards it
            {
                dir = (wPos - myPos).normalized;
                transform.Translate(dir * speed * Time.deltaTime, Space.World);
            }
        }
    }

    IEnumerator MoveToNext(float time)
    {
        waiting = true;
        yield return new WaitForSeconds(time);
        waiting = false;
        wpInd += delta;
        if (wpInd == waypoints.Count || wpInd == -1) //if we reached the last waypoint
        {
            if (mirrored)
            {
                delta *= -1;
                wpInd += delta * 2;
            }
            else
                wpInd = 0;
        }
        LookAt2D(waypoints[wpInd].target);
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

                if (mirrored && reachedEnd)
                    break;
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
        dir = n; //for forcing the right anim
        float angle = Mathf.Atan2(n.y, n.x) * Mathf.Rad2Deg;
        camTrans.rotation = Quaternion.Euler(0, 0, angle);
    }
}
