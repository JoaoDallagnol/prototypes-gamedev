using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    //Ressources
    public List<Sprite> playerSprite;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int experience;

    void Awake() {

        if (GameManager.instance != null) {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveState() {
        string s = "";
        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {

        if (!PlayerPrefs.HasKey("SaveState")){
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        pesos = int.Parse(data[1]);

        // Experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1){
            player.SetLevel(GetCurrentLevel());
        }
        

        // Change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        //Spawn the player
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon() {
        // is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel) {
            return false;
        }

        if (pesos >= weaponPrices[weapon.weaponLevel]) {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // Experience System
    public int GetCurrentLevel() {
        int r = 0;
        int add = 0;

        while (experience >= add) {
            add += xpTable[r];
            r++;

            // MAX LEVEL
            if (r == xpTable.Count) {
                return r;
            }
        }

        return r;
    }

    public int GetXpToLevel(int level) {
        int r = 0;
        int xp = 0;
        
        while (r < level) {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXp(int xp) {
        int currentLevel = GetCurrentLevel();
        experience += xp;
        if (currentLevel < GetCurrentLevel()) {
            OnLevelUp();
        }
    }

    public void OnLevelUp() {
        player.OnLevelUp();
    }
}
