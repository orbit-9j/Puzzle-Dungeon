using UnityEngine;
using Mirror;
public class Arrow : Collidable
{
    protected override void Start()
    {
        base.Start();
        filter.useLayerMask = true;
        filter.layerMask = LayerMask.GetMask("Blocking");
    }
    protected override void Update()
    {
        base.Update();
        transform.position += transform.up * Time.deltaTime * 6.0f;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.name != "Target")
            Destroy(gameObject);
    }
}