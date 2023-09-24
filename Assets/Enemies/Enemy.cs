using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;
    public float damage = 5;
    //public GameObject gameManager;
    private SpriteRenderer enemySprite;
    private float timeSinceDamaged = 1;
    private EnemyBehavior myEnemy;
    private float owTime;
    private float owTimeMax = 0.3f;

    public static float healthMultiplier = 1f;
   // private RoomManager roomManager;
    // Start is called before the first frame update
    void Start()
    {
        //roomManager = gameManager.GetComponent<RoomManager>();
        //roomManager.enemiesRemaining++;
        enemySprite = GetComponent<SpriteRenderer>();
        myEnemy = GetComponent<EnemyBehavior>();
        health *= healthMultiplier;

    }

    // Update is called once per frame
    void Update()
    {

        enemySprite.color = Color.Lerp(Color.red, Color.white, Mathf.Clamp(timeSinceDamaged, 0, 1f));
        if (timeSinceDamaged < 2f) { timeSinceDamaged += (Time.deltaTime * 2); };
        
        if (owTime > 0) { owTime -= Time.deltaTime; }
        myEnemy.ow = owTime > 0;
        //if (health <= 0) {
        //    death();
        //}
    }

    public static void IncreaseHealth() {
        healthMultiplier *= 1.2f;
    }


    public void TakeDamage(float dmg) {
        timeSinceDamaged = 0;
        health -= dmg;
        owTime = owTimeMax;
        if (health <= 0) {
            death();
        }
    }

    void death() {
        //roomManager.enemiesRemaining--;
        Destroy(gameObject);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }

         other.gameObject.GetComponent<Player>().receiveDamage(damage);
    }
}
