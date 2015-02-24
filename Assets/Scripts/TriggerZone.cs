using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class TriggerZone : MonoBehaviour 
{
    public List<string> tagsToReactTo;
    public UnityEvent onEnter, onExit;
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(tagsToReactTo.Contains(other.tag))
            onEnter.Invoke();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(tagsToReactTo.Contains(other.tag))
            onExit.Invoke();
    }
}
