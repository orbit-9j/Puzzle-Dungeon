using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//tutorial: https://www.youtube.com/watch?v=Mbb32TgMw0Q
public class MenuMultiplayer : NetworkBehaviour
{
    public NetworkManager networkManager;
    public GameObject menuPanel;
    public GameObject gamePanel;

    public void Host()
    {
        networkManager.StartHost();
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void SetIP(string ip)
    {
        networkManager.networkAddress = ip;
    }

    public void Join()
    {
        networkManager.StartClient();
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void Start()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void Stop()
    {
        if (networkManager.mode == NetworkManagerMode.Host)
        {
            networkManager.StopHost();
        }
        else if (networkManager.mode == NetworkManagerMode.ClientOnly)
        {
            networkManager.StopClient();
        }
    }
}
