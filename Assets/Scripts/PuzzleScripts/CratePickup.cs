using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror; 

public class CratePickup : NetworkBehaviour
{
    /* private bool holding = false; */
    private Transform BoxHolder;

    public void CrateManager(GameObject player)
    {
        BoxHolder = player.transform.Find("CrateHolder");
        if (gameObject.transform.parent == BoxHolder/* holding == false */){
            PickupCrate(player);
        }
        else
        {
            DropCrate(player);
        }
    }
    public void PickupCrate(GameObject player)
    {
        //also access player manager to check crate number
        BoxHolder = player.transform.Find("CrateHolder");
        gameObject.transform.parent = BoxHolder;
        gameObject.transform.position = BoxHolder.position;
        /* holding = true; */

    }

    public void DropCrate(GameObject player)
    {
        BoxHolder = player.transform.Find("CrateHolder");
        gameObject.transform.SetParent(null, true);
        gameObject.transform.position = BoxHolder.position;
        /* holding = false; */
    }
}
