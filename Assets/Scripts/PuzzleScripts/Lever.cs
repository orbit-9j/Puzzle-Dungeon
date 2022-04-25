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
    private SpriteRenderer sr;
    public Sprite leverLeft;
    public Sprite leverRight;
    [SyncVar(hook = nameof(OnLeverPulled))]
    private bool on = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    [Client]
    public void UseDoor(GameObject door)
    {
        // Not sure this is necessary
        CmdUseDoor(door);
    }

    [ClientCallback]
    private void OnLeverPulled(bool oldState, bool newState)
    {
        // Lever state has been updated, update the sprite accordingly (runs on client)
        if (on)
        {
            sr.sprite = leverRight;
        }
        else
        {
            sr.sprite = leverLeft;
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdUseDoor(GameObject door)
    {
        // Toggle the state of the lever and the connected door
        on = !on;
        door.GetComponent<ExitDoor>().CmdDoorToggle();
    }
}
