using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSpot : MonoBehaviour
{
    public Sprite digAvailable;
    public Sprite digHole;

    public void DigHole(GameObject player)//instead of renaming to DigKey or whatever and add new funct, add a new event with that function in the inspector
    {
        if (GetComponent<SpriteRenderer>().sprite == digAvailable)
        {
            GetComponent<SpriteRenderer>().sprite = digHole;
            PlayerManager manager = player.GetComponent<PlayerManager>();
            manager.PickupItem();

        }
    }
    
}
