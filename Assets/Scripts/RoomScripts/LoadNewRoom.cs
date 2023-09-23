using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewRoom : MonoBehaviour
{
    private Door _door;
    private Vector2[] _offsets = new Vector2[] { Vector2.up, Vector2.left, Vector2.down, Vector2.right };

    void Start()
    {
        _door = transform.parent.GetComponent<Door>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int side = _door.Side;
        Room currentRoom = GetComponentInParent<Room>();
        Vector2 currentPos = new Vector2(currentRoom.transform.position.x, currentRoom.transform.position.y);
        Vector2 newPos = currentPos + (currentRoom.roomSize) * _offsets[side];
        GameObject room = Instantiate(_door.NewRoom.gameObject, newPos, Quaternion.identity, currentRoom.transform.parent);

        Room r = room.GetComponent<Room>();
        r.Setup((side + 2) % 4);
        r.Doors[(side + 2) % 4].Exit();
        Room.MostRecentLoadedRoom = r;
        gameObject.SetActive(false);
    }

}
