using UnityEngine;

using Mirror;

public class TotemManager : NetworkBehaviour
{
    // The door to open
    public Openable openable;
    public Totem[] totems;
    public struct Flags
    {
        // Holds the value of whether the totem for a respective flag has been activated
        public bool red, purple, green, orange;
    }
    [SyncVar] // The value of this var should only be updated on the server, so all changes should propagate 
    public Flags flags = new Flags();

    public void Start()
    {
        foreach (Totem totem in totems)
        {
            totem.totemManager = GetComponent<TotemManager>();
        }
    }
    public void TryOpen()
    {
        if (flags.red && flags.orange && flags.green && flags.purple)
        {
            openable.Open();
        }
    }
}
