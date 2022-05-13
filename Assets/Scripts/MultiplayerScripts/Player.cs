using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using Cinemachine;
using Mirror;

public class Player : Mover
{
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private Text interactText;
    [SerializeField]
    private Canvas uiCanvas;

    public GameObject arrow;

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
    public void Update()
    {
        if (isLocalPlayer && Input.GetMouseButtonDown(0) && this.GetComponent<PlayerManager>().capabilities.ShootArrow)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 1.0f;
            Vector3 worldP = Camera.main.ScreenToWorldPoint(pos);
            worldP.z = 0.0f;
            GameObject spawnedArrow = Instantiate(arrow);
            NetworkServer.Spawn(spawnedArrow);
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(transform.position.y - worldP.y, transform.position.x - worldP.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad + 90;
            Quaternion spawnAngle = Quaternion.Euler(0, 0, AngleDeg);
            // Rotate Object
            Vector3 spawnPos = transform.position;
            CmdSpawnArrow(spawnPos, spawnAngle, this);
        }

    }
    [Command]
    private void CmdSpawnArrow(Vector3 pos, Quaternion angl, Player player)
    {
        if (isServer)
        {
            GameObject spawnedArrow = Instantiate(arrow);
            NetworkServer.Spawn(spawnedArrow);
            SetArrowPos(spawnedArrow, pos, angl);
        }
    }
    [ClientRpc]
    private void SetArrowPos(GameObject spawnedArrow, Vector3 pos, Quaternion angl)
    {
        spawnedArrow.transform.rotation = angl;
        spawnedArrow.transform.position = pos;
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

        // Cast a box, from the center of the mover's boxCollider, with the size of the boxCollider, 
        // along the y-axis, at a distance equal to that of the movement delta this frame. If this box
        // collides with an entity with the "Blocking" tag, we cannot move in this direction.
        RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2)transform.position + boxCollider.offset, boxCollider.size, 0, input, moveDelta.magnitude, LayerMask.GetMask("Blocking"));
        foreach (RaycastHit2D hit in hits)
        {
            moveDelta = moveDelta - (moveDelta * hit.normal) * hit.normal;
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


}

