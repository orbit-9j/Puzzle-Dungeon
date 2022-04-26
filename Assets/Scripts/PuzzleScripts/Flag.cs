using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class Flag : NetworkBehaviour
{
    [SyncVar]
    private bool pickedUp = false;
    public enum Colour { Red, Purple, Green, Orange } // Contains the possible colours for flags
    // Saves a bit of work, but not implemented as cleanly/extensibly as it could be
    public Colour colour = Colour.Red; // Default colour

    [Client]
    public void Capture()
    {
        // Run on the client, probably unnecessary 
        CmdCapture(NetworkClient.localPlayer.gameObject);
    }

    [Command(requiresAuthority = false)]
    private void CmdCapture(GameObject player)
    {
        // Runs on the server, tell the server we have taken a flag
        pickedUp = true;
        RpcUpdateFlag(player);
    }

    [ClientRpc]
    private void RpcUpdateFlag(GameObject player)
    {
        // Runs on client once the server has updated, disables the flag and adds to the PlayerManager
        gameObject.SetActive(false);
        if (NetworkClient.localPlayer.gameObject == player)
        {
            player.GetComponent<PlayerManager>().PickupFlag(colour);
        }
    }
}
