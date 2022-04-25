using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class Collidable : NetworkBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Check for collisions, once per frame
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] != null)
            {
                OnCollide(hits[i]);
            }
        }
        hits = new Collider2D[10];
    }


    protected virtual void OnCollide(Collider2D coll)
    {
        // The method called when this object collides with another collidable
    }
}
