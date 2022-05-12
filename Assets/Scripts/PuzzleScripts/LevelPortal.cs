using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : Collidable
{
    //connects to the portal (door) to take the player to the next scene
    //not networked
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            /* GameManager.instance.SaveState(); */
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
