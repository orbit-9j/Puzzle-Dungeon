using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class NPC : NetworkBehaviour
{
    [SyncVar]
    private bool hasGivenItem = false;
    [Client]
    public void RequestTrade()
    {
        // Only called by client, tries to trade if possible
        Debug.Log("Requesting Trade");
        GameObject player = NetworkClient.localPlayer.gameObject;
        PlayerManager manager = player.GetComponent<PlayerManager>();
        if (manager.isHoldingItem && !hasGivenItem)
        {
            Debug.Log("Executing Trade");
            // Player is holding the item required..
            CmdTradeItem(player);
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdTradeItem(GameObject player)
    {
        // Called on server, attempts to trade an item if one hasn't already been given
        RpcUpdateItem(player);
        hasGivenItem = true;
        Debug.Log("Traded items.");
    }

    [ClientRpc]
    private void RpcUpdateItem(GameObject player)
    {
        // Called by client, adds the correct flag and removes item if player initiated the trade
        if (NetworkClient.localPlayer.gameObject == player)
        {
            PlayerManager pm = player.GetComponent<PlayerManager>();
            pm.isHoldingItem = false;
            pm.PickupFlag(Flag.Colour.Orange);
        }
    }
}
