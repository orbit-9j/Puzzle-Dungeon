using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> expTable;

    //references
    public Player player;
    public FloatingTextManager floatingTextManager;

    //logic
    public int money;
    public int exp;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //save state
    public void SaveState()
    {
        //player skin, money, exp, weapon
        string s = "";
        s += "0" + "|" + money.ToString() + "|" + exp.ToString() + "|" + "0";

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("savestate");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        //SceneManager.sceneLoaded -= LoadState;
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        money = int.Parse(data[1]);
        exp = int.Parse(data[2]);
        

        Debug.Log("loadstate");
    }
}
