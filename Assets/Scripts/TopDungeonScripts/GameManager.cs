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

    //public Vector2Int roomCenter1 = RoomFirstDungeonGenerator.roomCenter;
    //private RoomFirstDungeonGenerator RFDG = GameObject.Find("RoomsFirstDungeonGeneratorGreen").GetComponent<RoomsFirstDungeonGenerator>.roomCenter;

    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //Spawn(RoomFirstDungeonGenerator.CreateRooms().roomCenters[0]);
        //Spawn(GameObject.Find("RoomsFirstDungeonGeneratorGreen").GetComponent<RoomsFirstDungeonGenerator>.CreateRooms().roomCenters[0]);
        //Spawn();
        //Spawn(RoomFirstDungeonGenerator.roomCenter);
        //Spawn(roomCenter1);
        //Spawn(RFDG.roomCenter);
        //SpawnPlayer(Vector2Int.zero);
        //SpawnPlayer(new Vector2Int(3,3));
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnPlayer(Vector2Int pos) //try to spawn a player in the middle of the first generated room. gives some error when called from the generator script (which wasn't there before, idk what i changed but it was half-working at some point and now it's not)
    {
       /*  Vector3 spawnPos = spawn.position;
        spawnPos.x = pos.x;
        spawnPos.y = pos.y;
        GameObject playerObject = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        cam.Follow = playerObject.transform; */

        //different approach: change the position of an existing object.
        //IT WORKS DON'T EVEN FUCKING BREATHE IN ITS DIRECTION 
        GameObject playerObject = GameObject.Find("Player");
        Vector3 playerObjectPos = playerObject.transform.position;
        playerObjectPos.x = pos.x;
        playerObjectPos.y = pos.y;
        playerObject.transform.position = playerObjectPos;
        //cam.Follow = playerObject.transform;

        //change camera colour to level colour
        //spawn character at roomCenters[0];
    }
/* 
    public void SpawnPlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab, spawn.position, Quaternion.identity);
        cam.Follow = playerObject.transform;
        //change camera colour to level colour
        //spawn character at roomCenters[0];
    } */

    //resources
    public List<Sprite> playerSprites;

    //references
    public Player player;


    //save state
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
