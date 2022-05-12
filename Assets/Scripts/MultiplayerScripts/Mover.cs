using UnityEngine;
using Mirror;

public abstract class Mover : NetworkBehaviour
{

    protected BoxCollider2D boxCollider;
    public RaycastHit2D hit;
    protected Vector2 speed = new Vector2(5.0f, 5.0f);
    public float slowFactor = 1.0f; // A factor by which movement is slowed (multiplier)
    public Animator animator;

    private SpriteRenderer sr;
    [SyncVar(hook = nameof(OnSpriteFlipped))] // Sync the direction the sprite is facing 
    private bool spriteFacingLeft;

    protected virtual void Start()
    {
        spriteFacingLeft = false;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    [ClientCallback]
    private void OnSpriteFlipped(bool oldVal, bool newVal)
    {
        GetComponent<SpriteRenderer>().flipX = spriteFacingLeft;
    }

    [Client]
    protected virtual void UpdateMotor(Vector2 input)
    {
        // The primary movement of entities like players
        input = input.normalized; // Normalise input to make diagonal movement have the same speed

        Vector2 moveBase = Vector2.Scale(input, speed) * slowFactor;

        if (moveBase.sqrMagnitude == 0) // If we are not moving..
        {
            animator.SetFloat("Speed", 0.0f); // ..set idle animation and return..
            return;
        }

        animator.SetFloat("Speed", 1.0f); // ..else set running animation and process movement below

        //Swap sprite direction
        if (moveBase.x > 0) // Moving left
        {
            CmdLookLeft();
        }

        else if (moveBase.x < 0) // Moving right
        {
            CmdLookRight();
        }

        Vector2 moveDelta = Time.deltaTime * moveBase;
        Vector2 centerPos = (Vector2)transform.position + boxCollider.offset;
        // Cast a box, from the center of the mover's boxCollider, with the size of the boxCollider, 
        // along the y-axis, at a distance equal to that of the movement delta this frame. If this box
        // collides with an entity with the "Blocking" tag, we cannot move in this direction.
        if (moveDelta.y != 0)
        {
            hit = Physics2D.BoxCast(centerPos, boxCollider.size, 0, new Vector2(0, 1), moveDelta.y, LayerMask.GetMask("Blocking"));
            if (hit.collider != null && !hit.collider.isTrigger)
            {
                moveDelta.y = 0;
            }
        }
        // Same as above, for x-direction
        if (moveDelta.x != 0)
        {
            hit = Physics2D.BoxCast(centerPos, boxCollider.size, 0, new Vector2(1, 0), moveDelta.x, LayerMask.GetMask("Blocking"));
            if (hit.collider != null && !hit.collider.isTrigger)
            {
                moveDelta.x = 0;
            }
        }
        transform.Translate(moveDelta);
    }

    [Command]
    protected void CmdLookLeft()
    {
        spriteFacingLeft = false;
    }
    [Command]
    protected void CmdLookRight()
    {
        spriteFacingLeft = true;
    }
}
