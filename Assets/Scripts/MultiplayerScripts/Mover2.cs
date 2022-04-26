using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Mover2 : NetworkBehaviour
{

    protected BoxCollider2D boxCollider;
    public float pushRecoverySpeed = 0.2f;

    protected Vector3 pushDirection;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 5.0f;
    protected float xSpeed = 5.0f;

    public Animator animator;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    protected virtual void UpdateMotor(Vector3 input)
    {

        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        if (moveDelta.sqrMagnitude == 0) // If we are not moving..
        {
            animator.SetFloat("Speed", 0.0f); // ..set idle animation and return..
            return;
        }

        animator.SetFloat("Speed", 1.0f); // ..else set running animation and process movement below

        //swap sprite direction
        if (moveDelta.x > 0) // Moving left
        {
            transform.localScale = Vector3.one;
        }

        else if (moveDelta.x < 0) // Moving right
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);


        //cast a box to check if can move in this direction
        hit = Physics2D.BoxCast(((Vector2)transform.position + (Vector2)boxCollider.offset), boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Dude", "Blocking"));
        if (hit.collider == null)
        {
            //movement
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0); //scales for machine's performance
        }

        hit = Physics2D.BoxCast(((Vector2)transform.position + (Vector2)boxCollider.offset), boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Dude", "Blocking"));
        if (hit.collider == null)
        {
            //movement
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0); //scales for machine's performance
        }
    }


}
