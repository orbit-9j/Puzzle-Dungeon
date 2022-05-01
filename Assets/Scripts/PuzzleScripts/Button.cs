using UnityEngine;
using Mirror;
public class Button : Switch
{
    public Openable openable;
    private bool active;
    protected override bool requiresKeyPress => false;

    [Client]
    protected override void InteractCallback()
    {
        if (!active)
        {
            active = true;
            Activate();
        }
    }
    [Client]
    protected override void LeaveCallback()
    {
        active = false;
        Deactivate();
    }
    [Command(requiresAuthority = false)]
    protected override void Activate()
    {
        base.Activate();
        openable.Open();
    }
    [Command(requiresAuthority = false)]
    protected override void Deactivate()
    {
        base.Deactivate();
        openable.Close();
    }
}
