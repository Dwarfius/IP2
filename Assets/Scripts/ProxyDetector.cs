using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProxyDetector : MonoBehaviour 
{
    public float startDetectRadius, fullDetectRadius;
    public string pickupTag;

    CircleCollider2D coll;
    List<GameObject> targetsInRange = new List<GameObject>();
    Slider slider;

	// Use this for initialization
	void Start () 
    {
        gameObject.AddComponent<Rigidbody2D>(); //needed to stop this collider from triggering the action in parent handler
        rigidbody2D.isKinematic = true;

        coll = gameObject.AddComponent<CircleCollider2D>();
        coll.radius = startDetectRadius;
        coll.isTrigger = true;
	}

    void Update()
    {
        if (!slider)
            return;

        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        targetsInRange.RemoveAll(x => x == null);
        if (targetsInRange.Count > 0)
        {
            //sorting by distance from this
            targetsInRange.Sort((x1, x2) => { return Vector2.Distance(x1.transform.position, myPos).CompareTo(Vector2.Distance(x2.transform.position, myPos)); });

            //filling the progress bar relative to distance to the object
            GameObject target = targetsInRange[0];
            Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
            float distance = Vector2.Distance(myPos, targetPos);
            float val = Mathf.Clamp01((distance - fullDetectRadius) / (startDetectRadius - fullDetectRadius));
            slider.value = 1 - val;
        }
        else
            slider.value = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == pickupTag)
            targetsInRange.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == pickupTag)
            targetsInRange.Remove(other.gameObject);
    }

    public void SetSlider(Slider s)
    {
        slider = s;
    }
}
