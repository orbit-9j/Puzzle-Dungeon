using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OurNetworkManager : NetworkManager
{
    public Transform playerSpawn;
    //private GameObject player;
    private List<GameObject> players = new List<GameObject>(); //list of player classes already instantiated

    void Start(){
        //shuffle list of spawnable player prefabs to randomly assign player class on join
        for (int i = 0; i < spawnPrefabs.Count - 1; i++)
        {
            GameObject temp = spawnPrefabs[i];
            int rand = Random.Range(i, spawnPrefabs.Count);
            spawnPrefabs[i] = spawnPrefabs[rand];
            spawnPrefabs[rand] = temp;
        }
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        playerSpawn = GameObject.Find("PlayerSpawn").transform;

        //go through each player class and check if it's already been spawned. if not, spawn it, if yes, move on to the next one. avoids duplicate classes
        for (int i = 0; i < spawnPrefabs.Count; i++) {
            //Debug.Log(players.Contains(spawnPrefabs[i])); //seems to break here
            if (!players.Contains(spawnPrefabs[i])){
                playerPrefab = spawnPrefabs[i];
                players.Add(spawnPrefabs[i]);
                break;
            }
            else{
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
