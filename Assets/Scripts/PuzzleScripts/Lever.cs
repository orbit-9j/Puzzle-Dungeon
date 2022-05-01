using UnityEngine;
using Mirror;

public class Lever : Switch
{
    // The most basic lever, implementing the Switch class. Attach an Openable,
    // and the lever will toggle the Openable's state when flipped.
    // Can also "Latch", meaning it will only turn on, and then won't turn off.
    [SerializeField]
    protected Openable openable;
    [SerializeField]
    protected bool latching = false;
    protected override void InteractCallback()
    {
        if (latching && state) { return; }
        base.InteractCallback();
        openable.Toggle();
    }
    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        if (latching && state)
        {
            return;
        }
        else { base.OnTriggerEnter2D(coll); }
    }
}
