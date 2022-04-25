using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class Totem : NetworkBehaviour
{
    public bool hasFlag = false;
    public GameObject flagSprite;
    protected TotemManager totemManager;

    public void EquipFlag()
    {
        GameObject player = NetworkClient.localPlayer.gameObject;
        totemManager = GameObject.Find("TotemManager").GetComponent<TotemManager>();

        if (!hasFlag)
        {
            PlayerManager manager = player.GetComponent<PlayerManager>();
            if (manager)
            {
                if (flagSprite.name == "PurpleFlag" && manager.purpleFlagCount > 0)
                {
                    hasFlag = true;
                    manager.UseFlag(flagSprite);
                    flagSprite.SetActive(true);
                    totemManager.purpleTotem = true;
                }

                else if (flagSprite.name == "RedFlag" && manager.redFlagCount > 0)
                {
                    hasFlag = true;
                    manager.UseFlag(flagSprite);
                    flagSprite.SetActive(true);
                    totemManager.redTotem = true;
                }

                else if (flagSprite.name == "OrangeFlag" && manager.orangeFlagCount > 0)
                {
                    hasFlag = true;
                    manager.UseFlag(flagSprite);
                    flagSprite.SetActive(true);
                    totemManager.orangeTotem = true;
                }

                else if (flagSprite.name == "GreenFlag" && manager.greenFlagCount > 0)
                {
                    hasFlag = true;
                    manager.UseFlag(flagSprite);
                    flagSprite.SetActive(true);
                    totemManager.greenTotem = true;
                }
            }
        }

    }
}
