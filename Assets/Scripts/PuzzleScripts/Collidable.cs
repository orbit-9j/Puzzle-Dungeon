using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class Collidable : NetworkBehaviour
{
    [SerializeField]
    protected ContactFilter2D filter;
    protected BoxCollider2D boxCollider;
    protected List<Collider2D> hits = new List<Collider2D>();
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Check for collisions, once per frame
        boxCollider.OverlapCollider(filter, hits);
        foreach (Collider2D hit in hits)
        {
            OnCollide(hit);
        }
        hits.Clear();
    }


    protected virtual void OnCollide(Collider2D coll)
    {
        // The method called when this object collides with another collidable
    }
}
