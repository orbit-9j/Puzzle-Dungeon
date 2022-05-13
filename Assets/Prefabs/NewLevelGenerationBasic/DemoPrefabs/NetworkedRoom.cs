using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkedRoom : NetworkBehaviour
{
    void Awake()
    {
        spawnChildPrefabs(transform.root);
    }

    void spawnChildPrefabs(Transform trans)
    {
        Debug.Log(trans.name);
        NetworkServer.Spawn(trans.gameObject);
        if (trans.childCount == 0 || trans.gameObject.GetComponent<NetworkIdentity>() != null)
        {   
            return;
        }
        foreach (Transform childTran in trans)
        {
            spawnChildPrefabs(childTran);
        }
    }
}
