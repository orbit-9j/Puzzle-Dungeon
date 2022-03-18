using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite leverOn;
    public Sprite leverOff;

   public void UseDoor(GameObject door)
   {/* if (coll.CompareTag == "Player"){} */
        
        if (GetComponent<SpriteRenderer>().sprite == leverOn){
            GetComponent<SpriteRenderer>().sprite = leverOff;
            door.GetComponent<ExitDoor>().DoorOpen();
        }
        else if(GetComponent<SpriteRenderer>().sprite == leverOff){
            GetComponent<SpriteRenderer>().sprite = leverOn;
            door.GetComponent<ExitDoor>().DoorClose();
        }  
   }
}
