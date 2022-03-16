using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public Sprite doorOpen;
    public Sprite doorClosed;
    void Start()
    {
        
        GameEvents.current.onDoorEnter += onDoorOpen;
        GameEvents.current.onDoorExit += onDoorClose;
    }

    private void onDoorOpen(){
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = doorOpen;
    }

    private void onDoorClose(){
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = doorClosed;
    }

    private void onDestroy(){
        GameEvents.current.onDoorEnter -= onDoorOpen;
        GameEvents.current.onDoorExit -= onDoorClose;
    }
}
