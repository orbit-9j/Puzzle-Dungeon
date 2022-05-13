using UnityEngine;
using Mirror;

public class Door : Openable
{
    // Child class of Openable, a door. Assign open and closed sprites in the inspector,
    // and then call Open/Close/Toggle.
    [SerializeField]
    protected Sprite doorOpen;
    [SerializeField]
    protected Sprite doorClosed;
    [Client]
    protected override void SetCloseState()
    {
        Debug.Log("Door");
        GetComponent<SpriteRenderer>().sprite = doorClosed;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    [Client]
    protected override void SetOpenState()
    {
        GetComponent<SpriteRenderer>().sprite = doorOpen;
        GetComponent<BoxCollider2D>().enabled = false;
    }

}
