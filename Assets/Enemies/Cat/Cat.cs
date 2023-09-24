using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public GameObject fireball;
    public Transform spawnPoint;

    private float fireballCD;
    private float fireballCDMax = 3f;

    private Rigidbody2D rb;
    private GameObject player;
    private EnemyBehavior myEnemy;

    // Start is called before the first frame update
    void Start()
    {
        myEnemy = GetComponent<EnemyBehavior>();
        player = myEnemy.player;
        rb = GetComponent<Rigidbody2D>();
        fireballCD = Random.Range(0.5f, 1.15f) * fireballCDMax * 0.25f;

    }

    // Update is called once per frame
    void Update()
    {
        //if (!myEnemy.activate || myEnemy.ow) { return; }
        Vector2 offset = (new Vector2(transform.position.x, transform.position.y) - new Vector2(player.transform.position.x, player.transform.position.y));
        float angle = (Mathf.Rad2Deg * Mathf.Atan2(offset.y, offset.x));
        rb.rotation = angle;

        if (fireballCD > 0) { 
            fireballCD -= Time.deltaTime; 
        } else { 
            fireballCD = Random.Range(0.9f, 1.15f) * fireballCDMax;
            GameObject fb = Instantiate(fireball, spawnPoint.position, transform.rotation, transform);
            Fireball fire = fb.GetComponent<Fireball>();
            fire.SetPlayer(myEnemy.player.GetComponent<Player>());
            fb.GetComponent<Rigidbody2D>().velocity = -transform.right * fire.fireballSpeed;
        }

        
    }
}
