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
    public RectTransform hitPointBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deathMenuAnim;

    // Logic
    public int pesos;
    public int experience;

    void Awake() {

        if (GameManager.instance != null) {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        SceneManager.sceneLoaded -= LoadState;

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
    }

    //On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode) {
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

    // Hitpoint Bar
    public void OnHitpointChange() {
        float ration = (float)player.hitPoint / (float)player.maxHitPoint;
        hitPointBar.localScale = new Vector3(1, ration, 1);
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
        OnHitpointChange();
    }

    // Death Menu and Respawn
    public void Respawn() {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }
}
