using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class TotemManager : NetworkBehaviour
{
    public struct Flags
    {
        // Holds the value of whether the totem for a respective flag has been activated
        public bool red, purple, green, orange;
    }
    [SyncVar]
    public Flags flags = new Flags();

    public void OpenDoor(GameObject door)
    {
        if (flags.red && flags.orange && flags.green && flags.purple)
        {
            door.GetComponent<ExitDoor>().CmdDoorOpen();
        }
    }
}
