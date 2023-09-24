using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNewRom : MonoBehaviour
{
    private Door _door;

    // Start is called before the first frame update
    void Start()
    {
        _door = transform.parent.GetComponent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ememy")) { return; }
        _door.Enter();
        //move camera
        Player.move = false;
        Transform parent = GetComponentInParent<Room>().transform;
        FollowRoom.Go(new Vector2(parent.position.x, parent.position.y));
        gameObject.SetActive(false);
        Room.numRoomsTillNote--;
        Room.numRoomsTillSlot--;
    }
   


}
