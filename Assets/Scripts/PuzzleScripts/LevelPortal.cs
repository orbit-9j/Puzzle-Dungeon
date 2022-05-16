using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class LevelPortal : Collidable
{
    
    //connects to the portal (door) to take the player to the next scene
    //not networked

    /* public NetworkManager networkManager;
    void Start(){
        networkManager = GameObject.Find("Network Manager").GetComponent<OurNetworkManager>();
    } */

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            /* GameManager.instance.SaveState(); */
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            //exits the game as there is currently only 1 level
            /* SceneManager.LoadScene("GameSelect"); */

            //tried to disconnect it, failed
            /* if (networkManager.mode == NetworkManagerMode.Host)
            {
                networkManager.StopHost();
            }
            else if (networkManager.mode == NetworkManagerMode.ClientOnly)
            {
                networkManager.StopClient();
            } */
        }
    }
}
