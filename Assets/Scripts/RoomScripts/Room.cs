using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Door[] Doors; //top left bot right
    public Vector2 roomSize = new Vector2(17.65f, 10.1f);
    public static int RoomNum = 0;
    public static Room MostRecentLoadedRoom;
    public static int numRoomsTillSlot;

    public GameObject enemy;
    public GameObject slotMachine;
    public List<int> randomsChosen;

    private List<GameObject> enemies;
    private int numEnemies;
    private float poissonRad = 0.5f;

    
    // Start is called before the first frame update
    void Start()
    {
        if (RoomNum == 0) {
            numRoomsTillSlot = Random.Range(3, 6);
            Setup(-1); //setup initial room
        }
    }

    public void Setup(int nono) {
        RoomNum++;
       
        foreach (Door d in Doors) {
            d.DoorInit();
        }
        randomsChosen = new List<int>();
        HashSet<int> chosen = new HashSet<int>();
        chosen.Add(nono);
        int numOpenRooms = Random.Range(1, Doors.Length + 1 - chosen.Count);
        while (numOpenRooms > 0) {
            //choose random room to open
            int random = Random.Range(0, Doors.Length);
            while (!chosen.Add(random)) {
                random = Random.Range(0, Doors.Length);
            }
            randomsChosen.Add(random);
            Door door = Doors[random];
            door.Enter();
            numOpenRooms--;
        }

        if (numRoomsTillSlot <= 0) {
            //no enemies
            //spawn machine
            Instantiate(slotMachine, transform.position, Quaternion.identity, transform);
            return;
        }

        if (nono != -1) { //don't spawn enemies for starting floor
            numEnemies = Random.Range(3, 6);
            PoissonThemEnemies();
        }

        
    }

    private void PoissonThemEnemies() {
        enemies = new List<GameObject>();

        for (int i = 0; i < numEnemies; i++)
        {
            int tries = 0;
            while (tries < 50) { 
                tries++;
                float maxHori = Random.Range(transform.position.x - 5f, transform.position.x + 5f);
                float maxVert = Random.Range(transform.position.y - 2f, transform.position.y + 1f);
                Vector2 pos = new Vector2(maxHori, maxVert);

                bool notAllowed = false;
                foreach(GameObject enemy in enemies) { 
                    if (Vector2.Distance(enemy.transform.position, pos) < poissonRad) {
                        notAllowed = true;
                        break;
                    }
                }

                if (notAllowed) {
                    continue;
                }

                GameObject e = Instantiate(enemy, pos, Quaternion.identity, transform);
                e.GetComponent<EnemyBehavior>().letTheRoomKnow += EnemyDown;
                enemies.Add(e);
                break;
            }
        }
    }

    public void ActivateEm() { 
        foreach(GameObject e in enemies) {
            e.GetComponent<EnemyBehavior>().activate = true;
        }
        enemies.Clear();
    }

    public void EnemyDown() {
        numEnemies--;
        if (numEnemies <= 0) { 
            foreach (int i in randomsChosen) {
                Door door = Doors[i];
                door.Enter();
            }
            
        }
    }

    


}
