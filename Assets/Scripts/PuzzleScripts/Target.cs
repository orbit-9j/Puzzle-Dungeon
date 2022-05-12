using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class Target : Lever
{
    public GameObject arrow;
    protected new bool latching = true;
    protected override void InteractCallback() { return; }

    [Client]
    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Arrows")
        {
            Destroy(coll.gameObject);
            CmdHitByArrow();
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdHitByArrow()
    {
        state = true;
        openable.Open();
        RpcHitByArrow();
    }
    [ClientRpc]
    private void RpcHitByArrow() { }

}