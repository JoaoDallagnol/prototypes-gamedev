using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {
    
    // Text fields
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick(bool right) {
        if (right) {
            currentCharacterSelection++;

            //If we went too far away
            if (currentCharacterSelection == GameManager.instance.playerSprite.Count) {
                currentCharacterSelection = 0;
            }

            OnSelectionChanged();
        } else {
            currentCharacterSelection--;

            //If we went too far away
            if (currentCharacterSelection < 0) {
                currentCharacterSelection = GameManager.instance.playerSprite.Count - 1;
            }

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged() {
        characterSelectionSprite.sprite = GameManager.instance.playerSprite[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }


    // Weapon Upgrade
    public void OnUpgradeClick() {
        if (GameManager.instance.TryUpgradeWeapon()) {
            UpdateMenu();
        }
    }

    // Update the character Information
    public void UpdateMenu() {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count) {
            upgradeCostText.text = "MAX";
        } else {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        

        //Meta
        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        //xp Bar
        int currentLevel = GameManager.instance.GetCurrentLevel();

        if (currentLevel == GameManager.instance.xpTable.Count) {
            xpText.text =  GameManager.instance.experience.ToString() + " total experience points!";
            xpBar.localScale = Vector3.one;
        } else {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentLevelXp = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currentLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale =  new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }
}
