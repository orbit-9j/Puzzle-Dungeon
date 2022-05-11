using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class IceBlock : Interactable
{
    private ParticleSystem particles;
    private SpriteRenderer sr;
    private BoxCollider2D boxCollider;

    [Client]
    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        foreach (BoxCollider2D bc in GetComponents<BoxCollider2D>())
        {
            if (!bc.isTrigger)
            {
                boxCollider = bc;
            }
        }
    }

    [Client]
    protected override void InteractCallback()
    {
        // Check if the player can break ice
        PlayerManager playerManager = NetworkClient.localPlayer.gameObject.GetComponent<PlayerManager>();
        if (playerManager.capabilities.BreakIce)
        {
            // Sends a message to the server to call Break on all clients
            CmdBreak();
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdBreak()
    {
        Break();
    }

    [ClientRpc]
    async private void Break()
    {
        // Play the animation, disable collision and rendering, then remove the object after a delay
        particles.Play();
        sr.enabled = false;
        boxCollider.enabled = false;
        int delay = (int)(particles.main.startLifetime.constantMax * 1000);
        await System.Threading.Tasks.Task.Delay(delay);
        gameObject.SetActive(false);
    }
}
