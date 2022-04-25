using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OurNetworkManager : NetworkManager
{
    public Transform playerSpawn;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        GameObject player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

}
