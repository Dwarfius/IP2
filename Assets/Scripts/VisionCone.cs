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
    bool detecting;
    float time;

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
        if(detecting)
        {
            time -= Time.deltaTime;
            lightCone.color = Color.Lerp(normalColor, detectColor, (timeToDetect - time) / timeToDetect);
            if(time < 0)
                Application.LoadLevel(Application.loadedLevel);
        }
        else if(time < timeToDetect)
        {
            time += Time.deltaTime;
            lightCone.color = Color.Lerp(normalColor, detectColor, (timeToDetect - time) / timeToDetect);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector2 dir = (col.transform.position - transform.position).normalized;
            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(transform.position, dir, radius))
                detecting = hit.transform.tag == "Player";
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            detecting = false;
    }
}
