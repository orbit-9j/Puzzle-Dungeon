/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collidable
{
    public Sprite emptyChest;
    public int money = 10;
   protected override void OnCollect()
   {
       if (!collected)
       {
           collected = true;
           GetComponent<SpriteRenderer>().sprite = emptyChest;
           GameManager.instance.money += money;
           GameManager.instance.ShowText("+" + money + " coins", 25, Color.yellow, transform.position, Vector3.up * 50, 2.0f);
       }
       
       //base.OnCollect();
       //Debug.Log("stonks");
   }
}
 */