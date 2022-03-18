using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : Collidable
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
    protected override void OnCollide(Collider2D coll)
   {
        if (coll.name == "Player")
        {
            StartCoroutine(Break());
        }
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
