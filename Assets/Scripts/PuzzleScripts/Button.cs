using UnityEngine;
using Mirror;
public class Button : Switch
{
    public Openable openable;
    protected override bool requiresKeyPress => false;

    [Client]
    protected override void InteractCallback()
    {
        Activate();
    }
    [Client]
    protected override void LeaveCallback()
    {
        Deactivate();
    }
    protected override void Activate()
    {
        base.Activate();
        openable.Open();
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        openable.Close();
    }
}
