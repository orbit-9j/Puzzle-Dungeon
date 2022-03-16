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

    public event Action<int> onDoorEnter;
    public void DoorEnter(int id)
    {
        onDoorEnter?.Invoke(id);
        //shorthand for:        
       /*  if (onDoorEnter != null)
        {
            onDoorEnter();
        } */
    }

    public event Action<int> onDoorExit;
    public void DoorExit(int id)
    {
        onDoorExit?.Invoke(id);
    }

    public event Action onFlagCapture;
    public void FlagCapture()
    {
        onFlagCapture?.Invoke();
    }

}
