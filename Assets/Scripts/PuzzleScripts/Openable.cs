using UnityEngine;
using Mirror;

public abstract class Openable : NetworkBehaviour
{
    [SyncVar]
    protected bool isOpen = false;

    [Command(requiresAuthority = false)]
    public virtual void Open()
    {
        isOpen = true;
        RpcUpdateOpenable();
    }

    [Command(requiresAuthority = false)]
    public virtual void Close()
    {
        isOpen = false;
        RpcUpdateOpenable();
    }
    [Command(requiresAuthority = false)]
    public virtual void Toggle()
    {
        if (isOpen) { Close(); }
        else { Open(); }
    }

    [ClientRpc]
    protected virtual void RpcUpdateOpenable()
    {
        if (isOpen)
        {
            SetOpenState();
        }
        else { SetCloseState(); }
    }

    protected abstract void SetOpenState();


    protected abstract void SetCloseState();
}
