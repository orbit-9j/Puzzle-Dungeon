using UnityEngine;
using Mirror;

public class Crate : Interactable
{
    // Sync both of these so each client knows if the crate is held, and who by
    [SyncVar]
    public bool holding = false;
    [SyncVar]
    public Player carrier = null;


    protected override void OnTriggerExit2D(Collider2D coll)
    {
        if (!holding)
        {
            base.OnTriggerExit2D(coll);
        }
    }
    [Client]
    protected override void InteractCallback()
    {
        Player localPlayer = NetworkClient.localPlayer.gameObject.GetComponent<Player>();
        if (holding && carrier == localPlayer)
        {
            // Local player is carrying the crate, try to drop it
            CmdDropCrate(localPlayer);
        }
        else if (!holding)
        {
            // Crate is not held, try to pick it up
            CmdPickupCrate(localPlayer);
        }
    }
    [Command(requiresAuthority = false)]
    private void CmdPickupCrate(Player player)
    {
        // Runs on server. Updates state and then returns the remote command on clients
        holding = true;
        carrier = player;
        RpcAttachCrate(player);
    }
    [Command(requiresAuthority = false)]
    private void CmdDropCrate(Player player)
    {
        // Runs on server. Updates state and then returns the remote command on clients
        holding = false;
        carrier = null;
        RpcDetachCrate(player);
    }
    [ClientRpc]
    private void RpcAttachCrate(Player player)
    {
        // Runs on client after command. Disables collision then attaches to carrier
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.transform.parent = player.transform;
        player.slowFactor = 0.5f;
    }
    [ClientRpc]
    private void RpcDetachCrate(Player player)
    {
        // Runs on client after command. Enables collision then detaches from carrier
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.transform.SetParent(null, true);
        player.slowFactor = 1.0f;
    }
}
