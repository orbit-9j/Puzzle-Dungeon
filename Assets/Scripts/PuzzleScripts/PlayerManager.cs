using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class PlayerManager : NetworkBehaviour
{
    [System.Serializable]
    public struct FlagCounter
    {
        // Contains the number of flags held of a respective colour
        public int Red, Green, Orange, Purple;
    }

    public FlagCounter flagCounts = new FlagCounter();

    public bool heldItem = false;



    public void PickupFlag(Flag.Colour colour)
    {
        // Player picked up a flag; increment the number of this flag held
        switch (colour)
        {
            case Flag.Colour.Red:
                flagCounts.Red += 1;
                break;
            case Flag.Colour.Green:
                flagCounts.Green += 1;
                break;
            case Flag.Colour.Orange:
                flagCounts.Orange += 1;
                break;
            case Flag.Colour.Purple:
                flagCounts.Purple += 1;
                break;
        }
    }


    public void UseFlag(Flag.Colour colour)
    {
        // Player used a flag; decrement the number of this flag held
        switch (colour)
        {
            case Flag.Colour.Red:
                flagCounts.Red -= 1;
                break;
            case Flag.Colour.Green:
                flagCounts.Green -= 1;
                break;
            case Flag.Colour.Orange:
                flagCounts.Orange -= 1;
                break;
            case Flag.Colour.Purple:
                flagCounts.Purple -= 1;
                break;
        }
    }


    public void PickupItem()
    {
        heldItem = true;
    }

}
