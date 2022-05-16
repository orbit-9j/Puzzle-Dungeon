using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//following the 3-part tutorial found at  https://www.youtube.com/watch?v=qAf9axsyijY
//sample code found at https://github.com/BlackthornProd/Random-Dungeon-Generation/tree/master/IsaacGenTut/Assets/Scripts  
public class RoomSpawner : MonoBehaviour
{
    public int openingDirection; //1 = B, 2 = T, 3 = L, 4 = R

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;
    public float waitTime = 5f; //keeping the wait time long allows all rooms to spawn before deleting spawn points, so rooms don't spawn on top of each other. a lazy way of doing this since some machines run slower than others.

    void Start()
    {
        Destroy(gameObject, waitTime); //delete spawn points (and their colliders) after rooms have spawned to save memory space
        templates = GameObject.FindGameObjectWithTag("Rooms")?.GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f); //call Spawn() with a time delay of 0.1 seconds to avoid all rooms spawning at once
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomSpawn"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedWall, transform.position, Quaternion.identity);
                //a room has already been instantiated at this position
                Destroy(gameObject); //don't spawn another room on top of it
            }
            spawned = true;
        }
    }
}
