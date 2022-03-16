using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    /* public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms; */
    

    public GameObject closedWall;

    public List<GameObject> rooms;

    public float waitTime;

    private bool spawnedDoor;

    public GameObject door;

    // Start is called before the first frame update
    /* void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SpawnPlayer(Vector2Int.zero);
    } */


    void Update()
    {
        if (waitTime <= 0 && spawnedDoor == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(door, rooms[i].transform.position, Quaternion.identity);
                }
                spawnedDoor = true;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
