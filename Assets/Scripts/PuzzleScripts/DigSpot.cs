using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class DigSpot : NetworkBehaviour
{
    public Sprite digAvailable;
    public Sprite digHole;
    private SpriteRenderer sr;
    [SyncVar]
    private bool diggable = true;

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    [Client]
    public void TryDig()
    {
        GameObject player = NetworkClient.localPlayer.gameObject;
        if (diggable)
        {
            CmdDig(player);
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdDig(GameObject player)
    {
        diggable = false;
        RpcDigItem(player);
    }

    [ClientRpc]
    private void RpcDigItem(GameObject player)
    {
        if (NetworkClient.localPlayer.gameObject == player)
        {
            PlayerManager pm = player.GetComponent<PlayerManager>();
            pm.PickupItem();
        }
        sr.sprite = digHole;
    }
}
