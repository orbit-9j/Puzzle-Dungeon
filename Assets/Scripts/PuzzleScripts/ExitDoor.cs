using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class ExitDoor : NetworkBehaviour
{
    public Sprite doorOpen;
    public Sprite doorClosed;
    [SyncVar(hook = nameof(OnOpenStateUpdated))]
    private bool isOpen = false;

    [Command(requiresAuthority = false)]
    public void CmdDoorOpen()
    {
        // Opens the door, called on the server
        isOpen = true;
    }

    [Command(requiresAuthority = false)]
    public void CmdDoorClose()
    {
        // Closes the door, called on the server
        isOpen = false;
    }

    [ClientCallback]
    private void OnOpenStateUpdated(bool oldState, bool newState)
    {
        // When the door state is changed, update the sprite and collider accordingly
        if (isOpen)
        {
            this.GetComponent<SpriteRenderer>().sprite = doorOpen;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = doorClosed;
            GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    [Command(requiresAuthority = false)]
    public void CmdDoorToggle()
    {
        // Update the state of the door (on the server)
        // This state update is hooked above, and will propagate to clients, updating sprite and collider
        isOpen = !isOpen;
    }
}
