using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {


    public Transform reveivingSprite;
	

    /// <summary>
    /// This method transports the player from the selected sprite (the one with this script attached) to the receiving one when the 
    /// player enters
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = reveivingSprite.position;
        }
    }
}
