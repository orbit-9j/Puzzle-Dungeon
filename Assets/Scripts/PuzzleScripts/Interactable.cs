using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent action;
    public UnityEvent onLeaveAction;
   

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if(Input.GetKeyDown(interactKey) || interactKey == KeyCode.None)
            {
                action.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll){
        if(coll.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            if(Input.GetKeyDown(interactKey) || interactKey == KeyCode.None)
            {
                onLeaveAction.Invoke(); //action to do as player exits interactable area. eg button un-pushes and door closes (as opposed to lever which has no counter-action and keeps the door open)
            }
        }
    }
}
