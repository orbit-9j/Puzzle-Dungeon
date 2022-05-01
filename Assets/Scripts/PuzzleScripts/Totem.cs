using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class Totem : Switch
{
    public Flag.Colour colour = Flag.Colour.Red; // Set a default colour
    public GameObject flagSprite;

    [Client]
    protected override void InteractCallback()
    {
        PlaceFlagOnTotem();
    }

    [Client]
    public void PlaceFlagOnTotem()
    {
        // Called only by clients
        NetworkIdentity player = NetworkClient.localPlayer; // Find the local player
        PlayerManager playerManager = player.GetComponent<PlayerManager>(); // Get the local PlayerManager
        if (!state)
        {
            if (playerManager)
            {
                switch (colour)
                // Depending on flag colour, add flag to the totem and consume the flag
                {
                    case Flag.Colour.Purple:
                        if (playerManager.flagCounts.Purple > 0)
                        {
                            CmdAddFlagToTotem(Flag.Colour.Purple);
                            playerManager.UseFlag(Flag.Colour.Purple);
                        }
                        break;
                    case Flag.Colour.Orange:
                        if (playerManager.flagCounts.Orange > 0)
                        {
                            CmdAddFlagToTotem(Flag.Colour.Orange);
                            playerManager.UseFlag(Flag.Colour.Orange);
                        }
                        break;
                    case Flag.Colour.Red:
                        if (playerManager.flagCounts.Red > 0)
                        {
                            CmdAddFlagToTotem(Flag.Colour.Red);
                            playerManager.UseFlag(Flag.Colour.Red);
                        }
                        break;
                    case Flag.Colour.Green:
                        if (playerManager.flagCounts.Green > 0)
                        {
                            CmdAddFlagToTotem(Flag.Colour.Green);
                            playerManager.UseFlag(Flag.Colour.Green);
                        }
                        break;
                }

            }
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdAddFlagToTotem(Flag.Colour colour)
    {
        // Runs on server, a lot of duplicated code, should be changed
        // Simply adds the correct flag to the totem, if appropriate
        TotemManager totemManager = GameObject.Find("TotemManager").GetComponent<TotemManager>();
        switch (colour)
        {
            case Flag.Colour.Red:
                totemManager.flags.red = true;
                break;
            case Flag.Colour.Orange:
                totemManager.flags.orange = true;
                break;
            case Flag.Colour.Purple:
                totemManager.flags.purple = true;
                break;
            case Flag.Colour.Green:
                totemManager.flags.green = true;
                break;
        }
        RpcSetFlagActive();
        state = true;
    }
    [ClientRpc]
    private void RpcSetFlagActive()
    {
        // Runs on client, activates the flag sprite if appropriate
        flagSprite.SetActive(true);
        GetComponent<Totem>().enabled = false;
    }
}
