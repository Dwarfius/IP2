using UnityEngine;
using System.Collections;

public class TutorialWriteOut : MonoBehaviour 
{
    public int charsPerSec = 10;
    public string[] messages;

    int currentMsgInd = -1;
    float timer;
    bool complete, display;

    public void DisplayNext()
    {
        currentMsgInd++;
        timer = 0;
        complete = false;
        display = true;
    }

    void Start()
    {
        for (int i = 0; i < messages.Length; i++)
            messages[i] += "\nPress Space to close this.";
    }

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            display = false;

        if(display && currentMsgInd > -1 && currentMsgInd < messages.Length)
        {
            timer += Time.deltaTime;
            int chars = (int)(timer * charsPerSec);
            complete = chars >= messages[currentMsgInd].Length;
             
            string currentMsg = complete ? messages[currentMsgInd] : messages[currentMsgInd].Substring(0, chars);
            Vector2 size = GUI.skin.box.CalcSize(new GUIContent(currentMsg));
            GUI.Box(new Rect((Screen.width - size.x) / 2, Screen.height * 7 / 8 - size.y / 2, size.x, size.y), currentMsg);
        }
    }
}
