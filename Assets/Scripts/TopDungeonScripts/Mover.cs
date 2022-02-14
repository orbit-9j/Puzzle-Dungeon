using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter //abstract - can't drag and drop on any object, must be inherited
{
    
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 5.0f;
    protected float xSpeed = 5.0f;

   protected virtual void Start()
   {
       boxCollider = GetComponent<BoxCollider2D>();
   }


   protected virtual void UpdateMotor(Vector3 input)
   {
       moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

       //swap sprite direction
       if(moveDelta.x > 0)
       {
           //transform.localScale = new Vector3(1,1,1);
           transform.localScale = Vector3.one;//saves memory
       }
       else if (moveDelta.x < 0)
       {
           transform.localScale = new Vector3(-1,1,1);
       }

        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);


        //cast a box to check if can move in this direction
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Dude", "Blocking"));
        if (hit.collider==null)
        {
            //movement
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0); //scales for machine's performance
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Dude", "Blocking"));
        if (hit.collider==null)
        {
            //movement
            transform.Translate( moveDelta.x * Time.deltaTime, 0, 0); //scales for machine's performance
        }
   }

    
}
