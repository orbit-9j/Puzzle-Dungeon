using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;


/* public event OnHarm onHarm;
[ClientRpc]
private void RpcOnHarm(float damage, GameObject attacker)
{
    this.onHarm?.Invoke(damage, attacker);
}

[Server]
private void Harm(float damage, GameObject attacker)
{
    // harm stuff here

    // invoke on server
    this.onHarm?.Invoke(damage, attacker); 
    // invoke on client
    RpcOnHarm(damage, attacker);
}


public class Flag : NetworkBehaviour
{
    private bool isUp = true;
    public event OnCollect onCollect;

    public void FlagCapture()
    {
        if(isUp)
        {
            GameObject player = NetworkClient.localPlayer.gameObject;
            PlayerManager manager = player.GetComponent<PlayerManager>();
            manager.PickupFlag(gameObject);
        //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    } */



public class Flag : NetworkBehaviour
{
    private bool isUp = true;

    public void FlagCapture()
    {
        if(isUp)
        {
            GameObject player = NetworkClient.localPlayer.gameObject;
            PlayerManager manager = player.GetComponent<PlayerManager>();
            manager.PickupFlag(gameObject);
        //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

}
