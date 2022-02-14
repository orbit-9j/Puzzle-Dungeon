using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //text fields
    public Text levelText, healthText, coinText, upgradeCostText, xpText;

    //logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //character selection
    public void OnArrowClick (bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }
        }
        else
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }         
        }

        OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    //weapon upgrade
    public void OnUpgradeClick()
    {

    }

    //update character info
    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.instance.weaponSprites[0];
        upgradeCostText.text = "rawr xD";
        healthText.text = GameManager.instance.player.hitPoint.ToString();
        coinText.text = GameManager.instance.money.ToString();
        levelText.text = "rawr xD";
        xpText.text = "rawr xD";
        xpBar.localScale = new Vector3(0.5f, 0, 0);
    }
}
