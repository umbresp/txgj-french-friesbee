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
        _door.Enter();
        //move camera
        Transform parent = GetComponentInParent<Room>().transform;
        FollowRoom.Go(new Vector2(parent.position.x, parent.position.y));
        gameObject.SetActive(false);
    }

}
