using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    
    private void Awake()
    {
        current = this;
    }

    public event Action onDoorEnter;
    public void DoorEnter()
    {
        if (onDoorEnter != null)
        {
            onDoorEnter();
        }
    }

    public event Action onDoorExit;
    public void DoorExit()
    {
        if (onDoorExit != null)
        {
            onDoorExit();
        }
    }
}
