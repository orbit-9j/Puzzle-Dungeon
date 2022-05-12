using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    
    public GameObject rightRoomExit;
    public GameObject leftRoomExit;
    public GameObject topRoomExit;
    public GameObject bottomRoomExit;
    public GameObject exitRoom;

    public GameObject closedWall; //supposed to prevent rooms with open exits, but it probably doesn't work

    public List<GameObject> rooms;

    private bool spawnedExit; 

    public float waitTime = 4; 
    

    void Update()
    {
        if (waitTime <= 0 && spawnedExit == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    //this approach doesn't always work because sometimes the last room in the list is not the last room. i don't wanna rewrite the algorithm so oh well ig lol
                    Transform SpawnL = rooms[i].transform.Find("SpawnPoint L");
                    Transform SpawnR = rooms[i].transform.Find("SpawnPoint R");
                    Transform SpawnT = rooms[i].transform.Find("SpawnPoint T");
                    Transform SpawnB = rooms[i].transform.Find("SpawnPoint B");

                    //because of this issue, i check if the current and previous rooms connect. if they don't i spawn a generic exit room with all exits to make sure that the exit room is never blocked off, like it sometimes happens if i don't do this check
                    Transform PrevSpawnL = rooms[i-1].transform.Find("SpawnPoint L");
                    Transform PrevSpawnR = rooms[i-1].transform.Find("SpawnPoint R");
                    Transform PrevSpawnT = rooms[i-1].transform.Find("SpawnPoint T");
                    Transform PrevSpawnB = rooms[i-1].transform.Find("SpawnPoint B");

                    if (SpawnL != null & PrevSpawnR != null){
                        Instantiate(rightRoomExit, rooms[i].transform.position, Quaternion.identity);

                    }
                    else if (SpawnR != null &  PrevSpawnL != null){
                        Instantiate(leftRoomExit, rooms[i].transform.position, Quaternion.identity);

                    }
                    else if (SpawnT != null & PrevSpawnB != null){
                        Instantiate(bottomRoomExit, rooms[i].transform.position, Quaternion.identity);

                    }
                    else if (SpawnB != null & PrevSpawnT != null){
                        Instantiate(topRoomExit, rooms[i].transform.position, Quaternion.identity);

                    }
                    else{
                        Instantiate(exitRoom, rooms[i].transform.position, Quaternion.identity); //generic room in case the rom generation fuckery happens
                    }
                    
                    Destroy(rooms[i]);
                    spawnedExit = true;
                    
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }


    /* public float waitTime;

    private bool spawnedDoor;

    public GameObject door;


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
    } */
}
