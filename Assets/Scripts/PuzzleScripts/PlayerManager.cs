using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public int redFlagCount;
    public int purpleFlagCount;
    public int greenFlagCount;
    public int orangeFlagCount;

    /* public string item; */
    public bool item = false;

    public void PickupFlag(GameObject obj){
        if (obj.name == "RedFlag"){
            redFlagCount++;
        }
        else if (obj.name == "PurpleFlag"){
            purpleFlagCount++;
        }
        else if (obj.name == "GreenFlag"){
            greenFlagCount++;
        }
        else if (obj.name == "OrangeFlag"){
            orangeFlagCount++;
        }
    }

    
    public void UseFlag(GameObject obj){
        //the flag count is actually checked in the totem script to get it to work properly, but i do it here too in case they will be used in a different script
        if (obj.name == "RedFlag" && redFlagCount > 0){
            redFlagCount--;
        }
        else if (obj.name == "PurpleFlag" && purpleFlagCount > 0){
            purpleFlagCount--;
        }
        else if (obj.name == "GreenFlag" && greenFlagCount > 0){
            greenFlagCount--;
        }
        else if (obj.name == "OrangeFlag" && orangeFlagCount > 0){
            orangeFlagCount--;
        }
    }


    public void PickupItem()
    {
        item = true;
    }

    /* ----------------old------------------ */
    public void PickupRedFlag()
    {
        if (redFlagCount > 0)
        redFlagCount++;
    }

    public void PickupPurpleFlag()
    {
        purpleFlagCount++;
    }

    public void PickupGreenFlag()
    {
        greenFlagCount++;
    }

    public void PickupOrangeFlag()
    {
        orangeFlagCount++;
    }

    public void useRedFlag()
    {
        redFlagCount--;
    }

    public void usePurpleFlag()
    {
        purpleFlagCount--;
    }

    public void useGreenFlag()
    {
        greenFlagCount--;
    }

    public void useOrangeFlag()
    {
        orangeFlagCount--;
    }
}
