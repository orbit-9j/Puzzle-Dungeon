using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror; 

public class DigSpot : NetworkBehaviour
{
    public Sprite digAvailable;
    public Sprite digHole;

    public void DigHole()
    {
        if (GetComponent<SpriteRenderer>().sprite == digAvailable)
        {
            GetComponent<SpriteRenderer>().sprite = digHole;

            GameObject player = NetworkClient.localPlayer.gameObject;
            //GameObject player = GameObject.Find("Player");
            PlayerManager manager = player.GetComponent<PlayerManager>();
        
            manager.PickupItem();

        }
    }
    
}
