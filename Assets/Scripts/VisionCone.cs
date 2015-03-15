using UnityEngine;
using System.Collections;

public class VisionCone : MonoBehaviour 
{
    public float radius;
    public float angle;
    public float timeToDetect = 0.5f;
    public Color normalColor = Color.white;
    public Color detectColor = Color.red;

    Light lightCone;
    float time;
    Transform player = null;

	void Start () 
    {
        Vector3 radV = new Vector3(radius, 0, 0);
        Vector3[] vertices = { Vector3.zero,
                               radV,
                               Quaternion.Euler(0, 0, angle) * radV,
                               Quaternion.Euler(0, 0, -angle) * radV };

        PolygonCollider2D col = gameObject.AddComponent<PolygonCollider2D>();
        Vector2[] points = { vertices[0], vertices[2], vertices[1], vertices[3] };
        col.points = points;
        col.isTrigger = true;

        lightCone = transform.GetChild(0).GetComponent<Light>();
        time = timeToDetect;
	}

    void Update()
    {
        if (player)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, radius);
            if (hit && hit.transform.tag == "Player")
            {
                time -= Time.deltaTime;
                lightCone.color = Color.Lerp(normalColor, detectColor, (timeToDetect - time) / timeToDetect);
                if(time < 0)
                    Application.LoadLevel(Application.loadedLevel);
                return;
            }
        }
        
        if(time < timeToDetect)
        {
            time += Time.deltaTime;
            lightCone.color = Color.Lerp(normalColor, detectColor, (timeToDetect - time) / timeToDetect);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            player = col.transform;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            player = null;
    }
}
