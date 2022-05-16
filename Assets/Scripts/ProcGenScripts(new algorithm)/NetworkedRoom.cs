using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkedRoom : NetworkBehaviour
{
    public void Awake()
    {
        CmdSpawnChildPrefabs(transform.root);
    }

    [Server]
    public void CmdSpawnChildPrefabs(Transform trans)
    {
        if (trans.gameObject.GetComponent<NetworkIdentity>() == null)
        {
            return;
        }
        NetworkServer.Spawn(trans.gameObject);
        if (trans.childCount == 0)
        {
            return;
        }
        foreach (Transform childTran in trans)
        {
            CmdSpawnChildPrefabs(childTran);
        }
    }
}
