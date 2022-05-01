using UnityEngine;
using Mirror;
public abstract class Interactable : NetworkBehaviour
{
    // The base interactable class
    [SerializeField]
    protected bool isInRange;
    [SerializeField]
    protected Collider2D interactCollider;
    [SerializeField]
    protected virtual bool requiresKeyPress => true; // A bool that stores whether you need to press a key to interact with this object
    [SerializeField]
    protected KeyCode interactKey = KeyCode.E;
    [SerializeField]
    protected string interactableText; // Text that is printed somewhere on the localplayer's UI when this item is highlighted
    [Client]
    void Update()
    {
        if (isInRange)
        {
            Player player = NetworkClient.localPlayer.gameObject.GetComponent<Player>();
            if (player.nearestInteractable.First.Value == GetComponent<Interactable>())
            { // This item is close to the player, and was the most recent one approached
                GetComponent<SpriteRenderer>().color = Color.grey; // Highlight
                if (Input.GetKeyDown(interactKey) || !requiresKeyPress)
                {
                    InteractCallback();
                }
            }
            else { GetComponent<SpriteRenderer>().color = Color.white; }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }
    [Client]
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collider = coll.gameObject;
        if (collider.CompareTag("Player")) // It's a player
        {
            Player player = collider.GetComponent<Player>();
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            { // It's the local player
                isInRange = true;
                player.nearestInteractable.AddFirst(gameObject.GetComponent<Interactable>());
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
                player.nearestInteractable.Remove(gameObject.GetComponent<Interactable>());
                player.HideInteractText();
                LeaveCallback();
            }
        }
    }

    protected abstract void InteractCallback(); // Called when the player interacts with the object
    protected virtual void LeaveCallback() { } // Called when the player leaves the object's trigger collider
}
