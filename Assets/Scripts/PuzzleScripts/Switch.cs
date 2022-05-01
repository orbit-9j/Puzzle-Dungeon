using UnityEngine;
using Mirror;
public abstract class Switch : Interactable
{
    // A simple Switch class. For toggle-state interactables like levers
    [SerializeField]
    protected Sprite spriteOn; // The sprite used when this Switch is in the on position
    [SerializeField]
    protected Sprite spriteOff; // The sprite used when this Switch is in the off position
    [SyncVar(hook = nameof(OnStateUpdate))]
    protected bool state = false;
    [Client]
    protected override void InteractCallback()
    {
        if (state)
        {
            Deactivate();
        }
        else { Activate(); }
    }

    [Command(requiresAuthority = false)]
    protected virtual void Activate()
    {
        state = true;
    }
    [Command(requiresAuthority = false)]
    protected virtual void Deactivate()
    {
        state = false;
    }
    [ClientCallback]
    private void OnStateUpdate(bool oldValue, bool newValue)
    {
        // The callback on the client when state is changed, update the sprite
        if (state)
        {
            GetComponent<SpriteRenderer>().sprite = spriteOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = spriteOff;
        }
    }
}
