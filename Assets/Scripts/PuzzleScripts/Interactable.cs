using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Mirror;
public class Interactable : MonoBehaviour
{
    [SerializeField]
    private bool isInRange;
    public KeyCode interactKey;
    public UnityEvent action;
    public UnityEvent onLeaveAction;

    [Client]
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey) || interactKey == KeyCode.None)
            {
                action.Invoke();
            }
        }
    }
    [Client]
    private void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collider = coll.gameObject;
        if (collider.CompareTag("Player")) // it's a player
        {
            if (collider.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                isInRange = true;
            }
        }
    }
    [Client]
    private void OnTriggerExit2D(Collider2D coll)
    {
        GameObject collider = coll.gameObject;
        if (collider.CompareTag("Player")) // it's a player
        {
            if (collider.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                isInRange = false;
                if (Input.GetKeyDown(interactKey) || interactKey == KeyCode.None)
                {
                    onLeaveAction.Invoke(); //action to do as player exits interactable area. eg button un-pushes and door closes (as opposed to lever which has no counter-action and keeps the door open)
                }
            }
        }
    }
}
