using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool item = false;
    public bool itemGiven = false;
    public void ExchangeItem(GameObject player){
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

    public void GiveOrangeFlag(GameObject player)
    {
        ExchangeItem(player);
        if (itemGiven)
        {
            PlayerManager manager = player.GetComponent<PlayerManager>();
            manager.PickupOrangeFlag();
            Debug.Log("Orange FLag!");
        }
    }
    
}
