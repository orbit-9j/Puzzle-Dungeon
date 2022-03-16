using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever2 : Collidable
{
    public int id;
    public Sprite leverLeft;
    public Sprite leverRight;

   private void OnTriggerEnter2D(Collider2D other){
       if (GetComponent<SpriteRenderer>().sprite == leverLeft){
           
            GetComponent<SpriteRenderer>().sprite = leverRight;
            GameEvents.current.DoorEnter(id);
       }
        
        else if (GetComponent<SpriteRenderer>().sprite == leverRight)
        {
            GetComponent<SpriteRenderer>().sprite = leverLeft;
            GameEvents.current.DoorExit(id);
        }

    }

}
