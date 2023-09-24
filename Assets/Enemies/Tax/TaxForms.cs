using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxForms : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fireball;
    private EnemyBehavior myEnemy;
    public Transform spawnPoint;

    private float fireballCD;
    private float fireballCDMax = 2f;
    void Start()
    {
        myEnemy = GetComponent<EnemyBehavior>();
        fireballCD = Random.Range(0.5f, 1.15f) * fireballCDMax * 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!myEnemy.activate || myEnemy.ow) { return; }

        if (fireballCD > 0) { 
            fireballCD -= Time.deltaTime; 
        } else { 
            fireballCD = Random.Range(0.9f, 1.15f) * fireballCDMax;
            GameObject fb = Instantiate(fireball, spawnPoint.position, transform.rotation, transform);
            Fireball fire = fb.GetComponent<Fireball>();
            fire.SetPlayer(myEnemy.player.GetComponent<Player>());
            fb.GetComponent<Rigidbody2D>().velocity = transform.right * fire.fireballSpeed;
        }

    }


}
