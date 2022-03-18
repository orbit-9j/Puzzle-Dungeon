using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Collidable
{
    public Sprite buttonOn;
    public Sprite buttonOff;

    //in case you need to keep a button pushed down for some reason
    /* public void ButtonOn(){
        GetComponent<SpriteRenderer>().sprite = buttonOn;
    }

    public void ButtonOff(){
        GetComponent<SpriteRenderer>().sprite = buttonOff;
    } */

    
    //in case this is actually useful for something
    /* private void OnTriggerEnter2D(Collider2D other){
        GetComponent<SpriteRenderer>().sprite = buttonOn;
    }

    private void OnTriggerExit2D(Collider2D other){
        GetComponent<SpriteRenderer>().sprite = buttonOff;
    } */

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
