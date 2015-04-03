using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class UnlockDoorScript : MonoBehaviour {
    public UnityEvent doorUnlock;
    
    bool doorIsUnlocked;

    public void UnlockTheDoor ()
    {
        if(!doorIsUnlocked)
        {
            doorUnlock.Invoke();
            doorIsUnlocked = true;
        }
    }
}
