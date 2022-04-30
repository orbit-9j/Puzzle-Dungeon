using UnityEngine;
using Mirror;
public abstract class Switch : Interactable
{
    [SerializeField]
    protected Sprite spriteOn;
    [SerializeField]
    protected Sprite spriteOff;
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
