using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

/* public class Lever : NetworkBehaviour
{
    public Sprite leverLeft;
    public Sprite leverRight;
    private bool on = false;

   public void UseDoor(GameObject door)
   {
       if (on == false){
            GetComponent<SpriteRenderer>().sprite = leverRight;
            on = true;
            door.GetComponent<ExitDoor>().DoorOpen();
            
        }
        else if(on == true){
            GetComponent<SpriteRenderer>().sprite = leverLeft;
            on = false;
            door.GetComponent<ExitDoor>().DoorClose();
        }    
   }
}
 */

 /* public class Lever : NetworkBehaviour
{
    public Sprite leverLeft;
    public Sprite leverRight;
    private bool on = false;

   public void UseDoor(GameObject door)
   {
       UseDoorServerRpc();      
   }

   [ServerRpc]
   private void UseDoorServerRpc()
   {
       UseDoorClientRpc();
   }

   [ClientRpc]
   private void UseDoorClientRpc()
   {
        if (on == false){
            GetComponent<SpriteRenderer>().sprite = leverRight;
            on = true;
            door.GetComponent<ExitDoor>().DoorOpen();
            
        }
        else if(on == true){
            GetComponent<SpriteRenderer>().sprite = leverLeft;
            on = false;
            door.GetComponent<ExitDoor>().DoorClose();
        }    
   }

} */

public class Lever : NetworkBehaviour
{
    public Sprite leverLeft;
    public Sprite leverRight;
    private bool on = false;

   public void UseDoor(GameObject door)
   {
       //if (!isLocalPlayer) return;
       //GetComponent<NetworkIdentity>().AssignClientAuthority(NetworkClient.localPlayer.gameObject.GetComponent<NetworkIdentity>().connectionToClient);
       CmdUseDoor(door);      
   }

   
    [Command]
    private void CmdUseDoor(GameObject door)
    {
        RpcLever(door);
    }

    [ClientRpc]
    public void RpcLever(GameObject door)
    {
        if (on == false){
                GetComponent<SpriteRenderer>().sprite = leverRight;
                on = true;
                door.GetComponent<ExitDoor>().DoorOpen();
                
            }
            else if(on == true){
                GetComponent<SpriteRenderer>().sprite = leverLeft;
                on = false;
                door.GetComponent<ExitDoor>().DoorClose();
            }    
            //GetComponent<NetworkIdentity>().RemoveClientAuthority(NetworkClient.localPlayer.gameObject.GetComponent<NetworkIdentity>().connectionToClient);
    }
}
