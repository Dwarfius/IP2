using UnityEngine;
using System.Collections;

public class TutorialWriteOut : MonoBehaviour 
{
    public int charsPerSec = 10;
    public string[] messages;

    int currentMsgInd = -1;
    float timer;
    bool complete;

    public void DisplayNext()
    {
        currentMsgInd++;
        timer = 0;
        complete = false;
    }

    void OnGUI()
    {
        if(currentMsgInd > -1 && currentMsgInd < messages.Length)
        {
            timer += Time.deltaTime;
            int chars = (int)(timer * charsPerSec);
            if (complete || chars < messages[currentMsgInd].Length)
            {
                string currentMsg = complete ? messages[currentMsgInd] : messages[currentMsgInd].Substring(0, chars);
                Vector2 size = GUI.skin.label.CalcSize(new GUIContent(currentMsg));
                GUI.Label(new Rect((Screen.width - size.x) / 2, Screen.height * 7 / 8 - size.y / 2, size.x, size.y), currentMsg);
            }
            else
                complete = true;
        }
    }
}
