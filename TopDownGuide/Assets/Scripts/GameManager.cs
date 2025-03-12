using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    //Ressources
    public List<Sprite> playerSprite;
    public List<Sprite> weaponSprite;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;

    // Logic
    public int pesos;
    public int experience;

    void Awake() {

        if (GameManager.instance != null) {
            Destroy(gameObject);
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
        s += "0";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {

        if (!PlayerPrefs.HasKey("SaveState")){
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
    }
}
