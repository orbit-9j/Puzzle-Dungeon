using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OurNetworkManager : NetworkManager
{
    public Transform playerSpawn;
    //private GameObject player;
    private List<GameObject> instantiatedPlayers = new List<GameObject>(); //list of player classes already instantiated
    public List<GameObject> availablePlayers = new List<GameObject>(); //must match the classes in registered objects

    //spawnPrefabs list
    public override void Start()
    {
        base.Start();
        //shuffle list of spawnable player prefabs to randomly assign player class on join
        for (int i = 0; i < availablePlayers.Count - 1; i++)
        {
            GameObject temp = availablePlayers[i];
            int rand = Random.Range(i, availablePlayers.Count);
            availablePlayers[i] = availablePlayers[rand];
            availablePlayers[rand] = temp;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        playerSpawn = GameObject.Find("PlayerSpawn").transform;

        //go through each player class and check if it's already been spawned. if not, spawn it, if yes, move on to the next one. avoids duplicate classes
        for (int i = 0; i < availablePlayers.Count; i++)
        {
            if (!instantiatedPlayers.Contains(availablePlayers[i]))
            {
                if (spawnPrefabs.Contains(availablePlayers[i])){ //instantiates equivalent prefab from the registered objects list
                    playerPrefab = spawnPrefabs[spawnPrefabs.IndexOf(availablePlayers[i])];
                    instantiatedPlayers.Add(availablePlayers[i]);
                    break;
                }
                /* playerPrefab = availablePlayers[i];
                instantiatedPlayers.Add(availablePlayers[i]);
                break; */
            }
            else
            {
                Debug.Log("player class already exists, spawning different one");
            }
        }

        //working code
        /* GameObject player = Instantiate(spawnPrefabs[0], playerSpawn.position, playerSpawn.rotation);
        NetworkServer.AddPlayerForConnection(conn, player); */

        GameObject player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    //ideally, delete the prefab from the list if a player disconnects so they can become that class again when they reconnect. but it doesn't work 
    //use base. whatever, maybe that will work
    /*  public override void OnStopClient(){
         GameObject localPlayer = NetworkClient.localPlayer.gameObject;
         players.Remove(localPlayer);
     } */
    /*  public override void OnClientDisconnect()
         {
             if (mode == NetworkManagerMode.Offline)
                 return;

             GameObject localPlayer = NetworkClient.localPlayer.gameObject;
             players.Remove(localPlayer);
             StopClient();
         } */
}
