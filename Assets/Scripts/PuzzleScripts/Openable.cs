using UnityEngine;
using Mirror;

public abstract class Openable : NetworkBehaviour
{
    // A simple openable class. For objects like doors, chests or similar.
    // Implements simple Open/Close/Toggle functionality, which is run on the server only
    // SetOpenState and SetClosedState are called on clients when the state is changed, so should
    // be used to do things like update sprites or local game state.
    [SyncVar, SerializeField]
    protected bool isOpen = false;

    [Command(requiresAuthority = false)]
    public virtual void Open()
    {
        isOpen = true;
        RpcUpdateOpenable(true);
    }

    [Command(requiresAuthority = false)]
    public virtual void Close()
    {
        isOpen = false;
        RpcUpdateOpenable(false);
    }
    [Command(requiresAuthority = false)]
    public virtual void Toggle()
    {
        if (isOpen) { Close(); }
        else { Open(); }
    }

    [ClientRpc]
    protected virtual void RpcUpdateOpenable(bool open)
    {
        if (open)
        {
            SetOpenState();
        }
        else { SetCloseState(); }
    }

    protected abstract void SetOpenState();


    protected abstract void SetCloseState();
}
