using UnityEngine;

using Mirror;

public class DigSpot : Switch
{
    private bool diggable = true;

    protected override void InteractCallback()
    {
        TryDig();
    }

    [Client]
    public void TryDig()
    {
        PlayerManager pm = NetworkClient.localPlayer.GetComponent<PlayerManager>();
        if (diggable && pm.capabilities.DigHole)
        {
            pm.PickupItem();
            CmdDig();
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdDig()
    {
        diggable = false;
        RpcDigItem();
    }

    [ClientRpc]
    private void RpcDigItem()
    {
        Activate();
    }
}
