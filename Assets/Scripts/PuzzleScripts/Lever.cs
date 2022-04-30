using UnityEngine;
using Mirror;

public class Lever : Switch
{
    public Openable openable;

    protected override void InteractCallback()
    {
        base.InteractCallback();
        openable.Toggle();
    }
}
