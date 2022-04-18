using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class TotemManager : NetworkBehaviour
{
    public bool redTotem = false;
    public bool purpleTotem = false;
    public bool greenTotem = false;
    public bool orangeTotem = false;

    public void OpenDoor(GameObject door)
    {
        if (redTotem == true && purpleTotem == true && greenTotem == true && orangeTotem == true)
        {
            door.GetComponent<ExitDoor>().DoorOpen();
        }
    }
}
