using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class IceBlock : NetworkBehaviour
{
    private ParticleSystem particles;
    private SpriteRenderer sr;
    private BoxCollider2D bc;


    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }
    [Command(requiresAuthority = false)]
    public void BreakIce()
    {
        // Sends a message to the server to call Break on all clients
        Break();
    }

    [ClientRpc]
    async private void Break()
    {
        // Play the animation, disable collision and rendering, then remove the object after a delay
        particles.Play();
        sr.enabled = false;
        bc.enabled = false;
        int delay = (int)(particles.main.startLifetime.constantMax * 1000);
        await System.Threading.Tasks.Task.Delay(delay);
        Destroy(gameObject);
    }
}
