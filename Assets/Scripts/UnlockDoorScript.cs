using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class UnlockDoorScript : MonoBehaviour {
    public UnityEvent doorUnlock;
    public bool doorIsUnlocked;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UnlockTheDoor ()
    {
        doorUnlock.Invoke();

    }
}
