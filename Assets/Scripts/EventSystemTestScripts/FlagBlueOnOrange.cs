using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBlueOnOrange : MonoBehaviour
{
    public Sprite flagOn;
    public Sprite flagDown;
    void Start()
    {
        
        GameEvents.current.onFlagCapture += onFlagCapture;
    }

    private void onFlagCapture(){
        if (GetComponent<SpriteRenderer>().sprite == flagOn)
        {
            GetComponent<SpriteRenderer>().sprite = flagDown;
            GameObject.Find("Door").GetComponent<DoorController>().enabled = true; //bad because it's dependent on the button existing. need to find a way around it using some sort of event chain
        }
    }


    private void onDestroy(){
        GameEvents.current.onFlagCapture -= onFlagCapture;
    }
}
