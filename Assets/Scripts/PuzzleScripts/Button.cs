using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Collidable
{
    [SerializeField]
    private Sprite buttonOn;
    [SerializeField]
    private Sprite buttonOff;

    public void OpenDoor(GameObject door)
    {
        GetComponent<SpriteRenderer>().sprite = buttonOn;
        door.GetComponent<ExitDoor>().DoorOpen();
    }

    public void CloseDoor(GameObject door)
    {
        GetComponent<SpriteRenderer>().sprite = buttonOff;
        door.GetComponent<ExitDoor>().DoorClose();
    }
}
