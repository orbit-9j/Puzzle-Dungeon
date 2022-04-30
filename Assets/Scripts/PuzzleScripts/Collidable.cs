using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class Collidable : NetworkBehaviour
{
    [SerializeField]
    private ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private List<Collider2D> hits = new List<Collider2D>();
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
        Debug.Log(coll);
        // The method called when this object collides with another collidable
    }
}
