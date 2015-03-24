using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {


    public Transform Spot;
	
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = Spot.position;
        }
    }
}
