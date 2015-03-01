using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class TriggerZone : MonoBehaviour 
{
    public bool fireOnce;
    public List<string> tagsToReactTo;
    public UnityEvent onEnter, onExit;

    bool enterFired, exitFired;
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enterFired && tagsToReactTo.Contains(other.tag))
        {
            onEnter.Invoke();
            if (fireOnce) 
                enterFired = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!exitFired && tagsToReactTo.Contains(other.tag))
        {
            onExit.Invoke();
            if (fireOnce) 
                exitFired = true;
        }
    }
}
