using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using Cinemachine;
using Mirror;

public class Player : Mover
{
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private Text interactText;
    [SerializeField]
    private Canvas uiCanvas;

    public LinkedList<Interactable> nearestInteractable = new LinkedList<Interactable>();

    [Client]
    protected override void Start()
    {
        base.Start();
        if (isLocalPlayer) // If this is the local player object, attach the camera
        {
            uiCanvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = this.transform;
        }
    }
    [Client]
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            //get values from keyboard (arrows and wasd)
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            UpdateMotor(new Vector2(x, y));
        }
    }
    [Client]
    public void ShowInteractText()
    {
        interactText.gameObject.SetActive(true);
    }
    [Client]
    public void ShowInteractText(string interactableText)
    {
        interactText.GetComponent<Text>().text = interactableText;
        interactText.gameObject.SetActive(true);
    }
    [Client]
    public void HideInteractText()
    {
        interactText.gameObject.SetActive(false);
    }
}
