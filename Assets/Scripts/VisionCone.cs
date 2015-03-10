using UnityEngine;
using System.Collections;

public class VisionCone : MonoBehaviour 
{
    public Material mat;
    public float radius;
    public float angle;

	void Start () 
    {
        Mesh m = new Mesh();
        Vector3 radV = new Vector3(radius, 0, 0);
        Vector3[] vertices = { Vector3.zero,
                               radV,
                               Quaternion.Euler(0, 0, angle) * radV,
                               Quaternion.Euler(0, 0, -angle) * radV };
        int[] indices = { 0, 2, 1, 0, 1, 3 };
        m.vertices = vertices;
        m.triangles = indices;
        gameObject.AddComponent<MeshFilter>().mesh = m;

        gameObject.AddComponent<MeshRenderer>().material = mat;
        //1 - 1.35, 2 - 2.21, 3 - 3.05, 4 - 4
        renderer.material.SetFloat("_Radius", 0.86f * radius + 0.515f); //this was gotten from linear regression for experimental results

        PolygonCollider2D col = gameObject.AddComponent<PolygonCollider2D>();
        Vector2[] points = { vertices[0], vertices[2], vertices[1], vertices[3] };
        col.points = points;
        col.isTrigger = true;
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector2 dir = (col.transform.position - transform.position).normalized;
            RaycastHit2D hit;
            if(hit = Physics2D.Raycast(transform.position, dir, radius))
            {
                if(hit.transform.tag == "Player")
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }
    }
}
