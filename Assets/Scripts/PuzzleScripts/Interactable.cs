using UnityEngine;
using Mirror;
public abstract class Interactable : NetworkBehaviour
{
    // The base interactable class
    // TODO: There needs to be some way for the player to only interact with a single (nearest/first) interactable,
    //       when they are in range of multiple. Possibly implement a list of in-range interactables on the player?
    [SerializeField]
    protected bool isInRange;
    [SerializeField]
    protected Collider2D interactCollider;
    [SerializeField]
    protected virtual bool requiresKeyPress => true;
    [SerializeField]
    protected KeyCode interactKey = KeyCode.E;
    [SerializeField]
    protected string interactableText;
    [Client]
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey) || !requiresKeyPress)
            {
                InteractCallback();
            }
        }

    }
    [Client]
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collider = coll.gameObject;
        if (collider.CompareTag("Player")) // it's a player
        {
            Player player = collider.GetComponent<Player>();
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                isInRange = true;
                if (interactableText != null)
                {
                    player.ShowInteractText(interactableText);
                }
                else { player.ShowInteractText(); }
            }
        }
    }
    [Client]
    protected virtual void OnTriggerExit2D(Collider2D coll)
    {
        GameObject collider = coll.gameObject;
        if (collider.tag == "Player")
        {
            Player player = collider.GetComponent<Player>();
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                isInRange = false;
                player.HideInteractText();
                LeaveCallback();
            }
        }
    }

    protected abstract void InteractCallback();
    protected virtual void LeaveCallback() { }
}
