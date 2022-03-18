using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private bool isUp = true;

    public void FlagCapture()
    {
        if(isUp)
        {
        //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

}
