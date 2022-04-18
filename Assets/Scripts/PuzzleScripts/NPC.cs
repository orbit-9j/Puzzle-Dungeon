using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror; 

public class NPC : NetworkBehaviour
{
    public bool item = false;
    public bool itemGiven = false;
    public void ExchangeItem(){
        GameObject player = NetworkClient.localPlayer.gameObject;
        //GameObject player = GameObject.Find("Player");
        PlayerManager manager = player.GetComponent<PlayerManager>();
        if (item == false && manager.item == false){
            Debug.Log("I need an item!");
        }
        else if (item == false && manager.item == true){
            manager.item = false;
            itemGiven = true;
            Debug.Log("Thanks for the item! Here's what I got for you:");
        }
        else{
            Debug.Log("I already have the item!");
        }
        //Debug.Log
    }

    public void GiveOrangeFlag()
    {
        ExchangeItem();
        if (itemGiven)
        {
            GameObject player = NetworkClient.localPlayer.gameObject;
            //GameObject player = GameObject.Find("Player");
            PlayerManager manager = player.GetComponent<PlayerManager>();
            manager.PickupOrangeFlag();
            itemGiven = false;
            Debug.Log("Orange FLag!");
        }
    }
    
}
