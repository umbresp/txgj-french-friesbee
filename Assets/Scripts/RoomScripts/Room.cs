using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Door[] Doors; //top left bot right
    public Vector2 roomSize = new Vector2(17.65f, 10.1f);
    public static int RoomNum = 0;
    public static Room MostRecentLoadedRoom;
    // Start is called before the first frame update
    void Start()
    {
        if (RoomNum == 0) {
            Setup(-1); //setup initial room
        }
    }

    public void Setup(int nono) {
        RoomNum++;
        foreach (Door d in Doors) {
            d.DoorInit();
        }
        HashSet<int> chosen = new HashSet<int>();
        chosen.Add(nono);
        int numOpenRooms = Random.Range(1, Doors.Length + 1 - chosen.Count);
        while (numOpenRooms > 0) {
            //choose random room to open
            int random = Random.Range(0, Doors.Length);
            while (!chosen.Add(random)) {
                random = Random.Range(0, Doors.Length);
            }
            Door door = Doors[random];
            door.Enter();
            numOpenRooms--;
        }
        
    }


}
