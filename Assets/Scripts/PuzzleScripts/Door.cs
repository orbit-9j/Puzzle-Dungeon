using UnityEngine;


public class Door : Openable
{
    [SerializeField]
    protected Sprite doorOpen;
    [SerializeField]
    protected Sprite doorClosed;

    protected override void SetCloseState()
    {
        GetComponent<SpriteRenderer>().sprite = doorClosed;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    protected override void SetOpenState()
    {
        this.GetComponent<SpriteRenderer>().sprite = doorOpen;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

}
