using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class ExitDoor : NetworkBehaviour
{ 
    public Sprite doorOpen;
    public Sprite doorClosed;
    /* public bool open = false; */

    public void DoorOpen()
    {
        GetComponent<SpriteRenderer>().sprite = doorOpen;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void DoorClose()
    {
        GetComponent<SpriteRenderer>().sprite = doorClosed;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
