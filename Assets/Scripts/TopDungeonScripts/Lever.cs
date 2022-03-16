using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* using System.Threading.Tasks; //for timer */

//it goes crazy switching between the sprites at 60 mph and idk how to stop it lmao fucking rip

public class Lever : Collidable
{
    public Sprite leverOn;
    public Sprite leverOff;

   protected override void OnCollide(Collider2D coll)
   {if (coll.name == "Player")
        {
            /* Task.Delay(1000); */
            if (GetComponent<SpriteRenderer>().sprite == leverOn){
                GetComponent<SpriteRenderer>().sprite = leverOff;
            }
            else if(GetComponent<SpriteRenderer>().sprite == leverOff){
                GetComponent<SpriteRenderer>().sprite = leverOn;
            }
            
            
        }
   }
}
