using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProxyDetector : MonoBehaviour 
{
    public float startDetectRadius, fullDetectRadius;
    public GameObject progressBarPrefab;

    CircleCollider2D coll;
    List<GameObject> targetsInRange = new List<GameObject>();
    Slider slider;

	// Use this for initialization
	void Start () 
    {
        coll = gameObject.AddComponent<CircleCollider2D>();
        coll.radius = startDetectRadius;
        coll.isTrigger = true;

        slider = (Instantiate(progressBarPrefab) as GameObject).transform.GetChild(0).GetComponent<Slider>();
	}

    void Update()
    {
        if (targetsInRange.Count > 0)
        {
            //sorting by distance from this
            targetsInRange.Sort((x1, x2) => { return (int)Vector3.Distance(x1.transform.position, transform.position); });

            //filling the progress bar relative to distance to the object
            GameObject target = targetsInRange[0];
            float distance = Vector3.Distance(target.transform.position, transform.position);
            float val = distance < fullDetectRadius ? 0 : distance / (startDetectRadius - fullDetectRadius);
            slider.value = 1 - val;
        }
        else
            slider.value = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Target")
            targetsInRange.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Target")
            targetsInRange.Remove(other.gameObject);
    }
}
