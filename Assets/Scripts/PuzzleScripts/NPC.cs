using Mirror;

public class NPC : Switch
{
    [SyncVar]
    private bool hasGivenItem = false;
    public Flag.Colour flagColour;
    protected override void InteractCallback()
    {
        RequestTrade();
    }
    [Client]
    public void RequestTrade()
    {
        // Only called by client, tries to trade if possible
        Player player = NetworkClient.localPlayer.GetComponent<Player>();
        PlayerManager manager = player.GetComponent<PlayerManager>();
        if (manager.isHoldingItem && !hasGivenItem)
        {
            // Player is holding the item required..
            CmdTradeItem();
            manager.PickupFlag(flagColour);
            manager.isHoldingItem = false;
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdTradeItem()
    {
        // Called on server
        hasGivenItem = true;
    }
}
