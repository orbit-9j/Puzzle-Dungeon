using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public bool hasFlag = false;
    public GameObject flagSprite;

    public void EquipFlag(GameObject player)
    {
        GameObject totemManager = GameObject.Find("TotemManager"); //change to totem manager
        
        if (!hasFlag)
        {
            PlayerManager manager = player.GetComponent<PlayerManager>();
            if(manager)
            {
                if (flagSprite.name == "PurpleFlag" && manager.purpleFlagCount > 0){
                    hasFlag = true;
                    manager.UseFlag(flagSprite);
                    flagSprite.SetActive(true);
                    totemManager.GetComponent<TotemManager>().purpleTotem = true;
                }

                else if (flagSprite.name == "RedFlag" && manager.redFlagCount > 0){
                    hasFlag = true;
                    manager.UseFlag(flagSprite);
                    flagSprite.SetActive(true);
                    totemManager.GetComponent<TotemManager>().redTotem = true;
                }
                
                else if (flagSprite.name == "OrangeFlag" && manager.orangeFlagCount > 0){
                    hasFlag = true;
                    manager.UseFlag(flagSprite);
                    flagSprite.SetActive(true);
                    totemManager.GetComponent<TotemManager>().orangeTotem = true;
                }

                else if (flagSprite.name == "GreenFlag" && manager.greenFlagCount > 0){
                    hasFlag = true;
                    manager.UseFlag(flagSprite);
                    flagSprite.SetActive(true);
                    totemManager.GetComponent<TotemManager>().greenTotem = true;
                }
            }
        }
        
    }
}
