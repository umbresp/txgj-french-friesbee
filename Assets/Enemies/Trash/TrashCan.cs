using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public GameObject explosion;
    private GameObject player;
    private EnemyBehavior myEnemy;
    private float minDist = 1.5f;
    private bool startExploding;

    private SpriteRenderer enemySprite;
    private int numFlashes = 0;
    private float ExplodingTime = 1;
    private bool goUp;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        myEnemy = GetComponent<EnemyBehavior>();
        rb = GetComponent<Rigidbody2D>();
        player = myEnemy.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (startExploding) {
            rb.velocity = Vector2.zero;
            enemySprite.color = Color.Lerp(Color.white, Color.black, Mathf.Clamp(ExplodingTime, 0, 0.6f));
            if (goUp) {
                ExplodingTime += Time.deltaTime * 2;
                if (ExplodingTime > 1f) {
                    goUp = false;
                    numFlashes++;
                    if (numFlashes == 2) {
                        Debug.Log("Explosion!");
                        GameObject exp = Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 270f), transform.parent);
                        exp.GetComponent<Explosion>().SetPlayer(player);
                        myEnemy.noEXP = true;
                        Destroy(gameObject);
                    }
                }
            } else {
                ExplodingTime -= Time.deltaTime * 2;
                if (ExplodingTime <= 0f) {
                    goUp = true;
                }
            }
            return;
        }

        if (!myEnemy.activate || myEnemy.ow) { return; }

        if (Vector2.Distance(player.transform.position, transform.position) < minDist) {
            myEnemy.activate = false;
            startExploding = true;
        }
    }
}
