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
        fireballCD = Random.Range(0.9f, 1.15f) * fireballCDMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (!myEnemy.activate || myEnemy.ow) { return; }

        if (fireballCD > 0) { 
            fireballCD -= Time.deltaTime; 
        } else { 
            fireballCD = Random.Range(0.9f, 1.15f) * fireballCDMax;
            GameObject fb = Instantiate(fireball, spawnPoint.position, Quaternion.identity, transform);
            Vector2 scale = fb.transform.localScale;
            scale.x = Mathf.Sign(transform.localScale.x) * Mathf.Abs(fb.transform.localScale.x);
            fb.transform.localScale = scale;
            Fireball fire = fb.GetComponent<Fireball>();
            fire.SetPlayer(myEnemy.player.GetComponent<Player>());
            fb.GetComponent<Rigidbody2D>().velocity = transform.right * fire.fireballSpeed;
        }

    }


}
