using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//it doesn't pop back up 
//honestly you could probably do it with an animator but i cba lol


public class ButtonRed : Collidable
{
    public Sprite buttonOn;
    public Sprite buttonOff;

   /*  protected override void Update(){
        GetComponent<SpriteRenderer>().sprite = buttonOff;
    }
     */
   protected override void OnCollide(Collider2D coll)
   {
        if (coll.name == "Player")
        {
            if(GetComponent<SpriteRenderer>().sprite == buttonOff){
                GetComponent<SpriteRenderer>().sprite = buttonOn;
            }
        }
   }
   
}
