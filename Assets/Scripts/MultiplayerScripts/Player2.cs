using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player2 : Mover2
{
    private void Start()
    {
        
        if (isLocalPlayer)
        {
            
       boxCollider = GetComponent<BoxCollider2D>();
            CinemachineVirtualCamera camera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            camera.Follow = transform;
        }
    }
    private void FixedUpdate()
    {
        if (isLocalPlayer)
       {
        //get values from keyboard (arrows and wasd)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x,y,0));
       }
       
    }
}
