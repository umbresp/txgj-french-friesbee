using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoom : MonoBehaviour
{
    public float moveSpeed;
    public Transform roomFolder;
    private static Vector3 tgt;
    private static bool Follow;
    private static float zPos;
    // Start is called before the first frame update
    void Start()
    {
        zPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Follow) { return; }
        transform.position = Vector3.MoveTowards(transform.position, tgt, moveSpeed * Time.deltaTime);
        if ((transform.position - tgt).magnitude < 0.1f ) {
            //when the camera stops do all of this
            Follow = false;
            Room recent = Room.MostRecentLoadedRoom;
            foreach (Transform child in roomFolder) {
                if (child.gameObject != recent.gameObject) {
                    Destroy(child.gameObject);
                }
            }
            Player.move = true;

            if (recent.ActivateEm()) { 
                 foreach (Door d in recent.Doors) {
                    d.DoorInit();
                }
            }
           
            //if (Room.numRoomsTillNote >= 0 && Room.numRoomsTillSlot >= 0) { 
            //    recent.ActivateEm();
            //    foreach (Door d in recent.Doors) {
            //        d.DoorInit();
            //    }
            //}
            
        }
    }

    public static void Go(Vector2 newPos) {
        tgt = new Vector3(newPos.x, newPos.y, zPos);
        Follow = true;
    }
}
