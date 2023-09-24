using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Door[] Doors; //top left bot right
    public Vector2 roomSize = new Vector2(17.65f, 10.1f);
    //alos HOLY shit look at all these statics im tired
    public static int RoomNum = 0;
    public static Room MostRecentLoadedRoom;
    public static int numRoomsTillSlot;
    public static HashSet<int> whichNotesChosen;
    public static int numRoomsTillNote;
    //EXTREME DUCTTAPE for bug IDK
    public Notes[] notes;
    public GameObject vendingmachine;
    public Sprite[] doorSprites;

    public GameObject[] enemyPrefabs;
    //public GameObject enemy;
    //public GameObject slotMachine;
    public List<int> randomsChosen;
    public static int minEnemies;
    public static int maxEnemies;

    private List<GameObject> enemies;
    private int numEnemies;
    private float poissonRad = 0.5f;

    
    // Start is called before the first frame update
    void Start()
    {
        if (RoomNum == 0) {
            //setup initial room
            minEnemies = 1;
            maxEnemies = 4;
            whichNotesChosen = new HashSet<int>();
            numRoomsTillSlot = Random.Range(3, 5);
            numRoomsTillNote = Random.Range(6, 8);
            Setup(-1); 
            //SetupNotesList();
        }
    }

    public void Setup(int nono) {
        RoomNum++;

        foreach(Notes n in notes) {
            n.gameObject.SetActive(false);
        }
        vendingmachine.SetActive(false);
        int randomDoorI = Random.Range(0, doorSprites.Length);
        foreach (Door d in Doors) {
            d.DoorInit(doorSprites[randomDoorI]);
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

        if (numRoomsTillNote <= 0) {
            Debug.Log("Hey! Note");
            int random = Random.Range(0, notes.Length);
            if (whichNotesChosen.Count != notes.Length) { 
                while (!whichNotesChosen.Add(random)) {
                    random = Random.Range(0, notes.Length);
                }
            }
            notes[random].gameObject.SetActive(true);
            numRoomsTillNote = Random.Range(6, 8);
            return;
        }


        if (numRoomsTillSlot <= 0) {
            //no enemies
            //spawn machine
            Debug.Log("Machine");
            vendingmachine.SetActive(true);
            numRoomsTillSlot = Random.Range(3, 6);
            return;
        }

        if (nono != -1) { //don't spawn enemies for starting floor
            if (RoomNum % 4 == 0) {
                //increase the number of allowed enemies by 2?
                minEnemies += 2;
                maxEnemies += 4;
            }
            numEnemies = Random.Range(minEnemies, maxEnemies);
            enemies = new List<GameObject>();
            DecideSpawns();
        }
        
    }

    private void DecideSpawns() {
        float percentage;
        int numE;
        if (RoomNum < 4) {
            PoissonThemEnemies(enemyPrefabs[0], numEnemies);
            return;
        }
        if (RoomNum < 10) {
            percentage = Random.Range(0f, 1f);
            numE = (int) (numEnemies * percentage);
            PoissonThemEnemies(enemyPrefabs[0], numE);
            PoissonThemEnemies(enemyPrefabs[1], numEnemies - numE);
            return;
        }
        percentage = Random.Range(0f, 1f);
        numE = (int)(numEnemies * percentage);
        PoissonThemEnemies(enemyPrefabs[0], numE);
        PoissonThemEnemies(enemyPrefabs[1], numEnemies - numE);
        int numBombs = Random.Range(0, 2 + (RoomNum / 10));
        numEnemies += numBombs;
        PoissonThemEnemies(enemyPrefabs[2], numBombs);
        if (RoomNum < 15) {
            int numCats = Random.Range(0, 1 + (RoomNum / 10));
            numEnemies += numCats;
            PoissonThemEnemies(enemyPrefabs[3], numCats);
        }
        return;
    }

    private void PoissonThemEnemies(GameObject enemy, int num) {

        for (int i = 0; i < num; i++)
        {
            int tries = 0;
            while (tries < 50) { 
                tries++;
                float maxHori = Random.Range(transform.position.x - 5f, transform.position.x + 5f);
                float maxVert = Random.Range(transform.position.y - 2f, transform.position.y + 1f);
                Vector2 pos = new Vector2(maxHori, maxVert);

                bool notAllowed = false;
                foreach(GameObject en in enemies) { 
                    if (Vector2.Distance(en.transform.position, pos) < poissonRad) {
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

    public bool ActivateEm() { 
        if (enemies == null) { return false; }
        foreach(GameObject e in enemies) {
            e.GetComponent<EnemyBehavior>().activate = true;
        }
        enemies.Clear();
        return true;
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
