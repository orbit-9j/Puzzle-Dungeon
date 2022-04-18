using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OurNetworkManager : NetworkManager
{
    public Transform playerSpawn;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform start = playerSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

}
