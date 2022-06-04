using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public Transform spawn;
    public GameObject playerPrefab;

    public CinemachineVirtualCameraBase cam;

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

    public void SpawnPlayer(Vector2Int pos) 
    {
        //different approach: change the position of an existing object.
        GameObject playerObject = GameObject.Find("Player");
        Vector3 playerObjectPos = playerObject.transform.position;
        playerObjectPos.x = pos.x;
        playerObjectPos.y = pos.y;
        playerObject.transform.position = playerObjectPos;
    }

    public void SpawnPlayer() //try to spawn a player in the middle of the first generated room. gives some error when called from the generator script (which wasn't there before, idk what i changed but it was half-working at some point and now it's not)
    {
        Vector3 spawnPos = spawn.position;
        spawnPos.x = Vector2Int.zero.x;
        spawnPos.y = Vector2Int.zero.y;
        GameObject playerObject = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        cam.Follow = playerObject.transform;
    }

    public List<Sprite> playerSprites;
    public Player player;

    public void SaveState()
    {
        //player skin, money, exp, weapon
        string s = "";
        s += "0";

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
        
        //Debug.Log("loadstate");
    }
}
