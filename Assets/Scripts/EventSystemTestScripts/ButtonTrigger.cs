using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : Collidable
{
    public Sprite buttonOn;
    public Sprite buttonOff;

    private void OnTriggerEnter2D(Collider2D other){
        GetComponent<SpriteRenderer>().sprite = buttonOn;
        GameEvents.current.DoorEnter();
    }

    private void OnTriggerExit2D(Collider2D other){
        GetComponent<SpriteRenderer>().sprite = buttonOff;
        GameEvents.current.DoorExit();
    }
}
