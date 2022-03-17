using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePickup : Collidable
{
    public Transform grabDetect;
    public Transform BoxHolder;
    public float rayDist;

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        if (grabCheck.collider != null && grabCheck.collider.tag == "Crate")
        {
            grabCheck.collider.gameObject.transform.parent = BoxHolder;
            grabCheck.collider.gameObject.transform.position = BoxHolder.position;
        }
        /* else
        {
            grabCheck.collider.gameObject.transform.parent = null;
        } */
    }
}
