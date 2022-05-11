using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using Cinemachine;
using Mirror;

public class Player : Mover
{
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private Text interactText;
    [SerializeField]
    private Canvas uiCanvas;

    [SerializeField]
    private BoxCollider2D topOnly;
    [SerializeField]
    private BoxCollider2D topLeft;
    [SerializeField]
    private BoxCollider2D topRight;
    [SerializeField]
    private BoxCollider2D rightOnly;
    [SerializeField]
    private BoxCollider2D bottomRight;
    [SerializeField]
    private BoxCollider2D bottomOnly;
    [SerializeField]
    private BoxCollider2D bottomLeft;
    [SerializeField]
    private BoxCollider2D leftOnly;

    public LinkedList<Interactable> nearestInteractable = new LinkedList<Interactable>();

    [Client]
    protected override void Start()
    {
        base.Start();
        if (isLocalPlayer) // If this is the local player object, attach the camera
        {
            uiCanvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = this.transform;
        }
    }
    [Client]
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            //get values from keyboard (arrows and wasd)
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            UpdateMotor(new Vector2(x, y));
        }
    }

    [Client]
    protected override void UpdateMotor(Vector2 input)
    {
        // The primary movement of the player
        Vector2 normalizedInput = input.normalized; // Normalise input to make diagonal movement have the same speed
        Vector2 moveBase = Vector2.Scale(normalizedInput, speed) * slowFactor;
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
        //Vector2 centerPos = (Vector2)transform.position + boxCollider.offset;
        BoxCollider2D horizontalColl = null;
        BoxCollider2D verticalColl = null;
        BoxCollider2D cornerColl = null;

        // Cast a box, from the center of the mover's boxCollider, with the size of the boxCollider, 
        // along the y-axis, at a distance equal to that of the movement delta this frame. If this box
        // collides with an entity with the "Blocking" tag, we cannot move in this direction.
        if (moveDelta.y > 0 && moveDelta.x > 0)
        {

            cornerColl = topRight;
            horizontalColl = rightOnly;
            verticalColl = topOnly;
        }
        else if (moveDelta.y > 0 && moveDelta.x < 0)
        {

            cornerColl = topLeft;
            horizontalColl = leftOnly;
            verticalColl = topOnly;
        }
        else if (moveDelta.y > 0 && moveDelta.x == 0)
        {

            verticalColl = topOnly;
        }
        else if (moveDelta.y == 0 && moveDelta.x > 0)
        {

            horizontalColl = rightOnly;
        }
        else if (moveDelta.y == 0 && moveDelta.x < 0)
        {

            horizontalColl = leftOnly;
        }
        else if (moveDelta.y < 0 && moveDelta.x == 0)
        {

            verticalColl = bottomOnly;
        }
        else if (moveDelta.y < 0 && moveDelta.x < 0)
        {

            cornerColl = bottomLeft;
            horizontalColl = leftOnly;
            verticalColl = bottomOnly;
        }
        else // To appease the compiler
        {

            cornerColl = bottomRight;
            horizontalColl = rightOnly;
            verticalColl = bottomOnly;
        }
        // Vertical movement check
        LayerMask blockingMask = LayerMask.GetMask("Blocking");
        if (verticalColl != null)
        {
            hit = BoxCast((Vector2)transform.position + verticalColl.offset, verticalColl.size, 0, new Vector2(0, input.y), moveDelta.y, blockingMask);
            if (hit.collider != null && !hit.collider.isTrigger)
            {
                moveDelta.y = 0;
            }
        }
        if (horizontalColl != null)
        {
            hit = BoxCast((Vector2)transform.position + horizontalColl.offset, horizontalColl.size, 0, new Vector2(input.x, 0), moveDelta.x, blockingMask);
            if (hit.collider != null && !hit.collider.isTrigger)
            {
                moveDelta.x = 0;
            }
        }
        if (cornerColl != null)
        {
            // hit = BoxCast((Vector2)transform.position + cornerColl.offset, cornerColl.size, 0, input, moveDelta.magnitude, blockingMask);
            // if (hit.collider != null && !hit.collider.isTrigger)
            // {
            //     moveDelta = Vector2.zero;
            // }
        }
        GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + moveDelta);
    }

    [Client]
    public void ShowInteractText()
    {
        interactText.gameObject.SetActive(true);
    }
    [Client]
    public void ShowInteractText(string interactableText)
    {
        interactText.GetComponent<Text>().text = interactableText;
        interactText.gameObject.SetActive(true);
    }
    [Client]
    public void HideInteractText()
    {
        interactText.gameObject.SetActive(false);
    }

    static public RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance, int mask)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance, mask);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }
}

