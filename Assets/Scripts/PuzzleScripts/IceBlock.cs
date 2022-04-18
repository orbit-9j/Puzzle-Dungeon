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
    public void BreakIce()
   {
        StartCoroutine(Break());
   }

   private IEnumerator Break()
   {
    particles.Play();
    sr.enabled = false;
    bc.enabled = false;
    yield return new WaitForSeconds(particles.main.startLifetime.constantMax);
    Destroy(gameObject);
   }
}
